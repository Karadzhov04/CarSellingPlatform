using CarSellingPlatform.ActionFilters;
using CarSellingPlatform.ViewModels.Home;
using Common.Entities;
using Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarSellingPlatform.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index(IndexVM filter)
        {
            using (CarSellingPlatformDbContext context = new CarSellingPlatformDbContext())
            {
                // Създаване на филтъра като Expression Tree
                Expression<Func<Car, bool>> carFilter = c =>
                    (!filter.Brand.HasValue || c.Brand == filter.Brand.Value) &&
                    (string.IsNullOrEmpty(filter.Model) || c.Model.ToLower().Contains(filter.Model.ToLower())) &&
                    (string.IsNullOrEmpty(filter.Year) || c.Year == filter.Year) &&
                    (!filter.MinPrice.HasValue || c.Price >= filter.MinPrice.Value) &&
                    (!filter.MaxPrice.HasValue || c.Price <= filter.MaxPrice.Value) &&
                    (!filter.Engine.HasValue || c.Engine == filter.Engine.Value) &&
                    (!filter.MinMileage.HasValue || c.MileageInKm >= filter.MinMileage.Value) &&
                    (!filter.MaxMileage.HasValue || c.MileageInKm <= filter.MaxMileage.Value) &&
                    (!filter.MinHorsePower.HasValue || c.HorsePower >= filter.MinHorsePower.Value) &&
                    (!filter.MaxHorsePower.HasValue || c.HorsePower <= filter.MaxHorsePower.Value);

                // Прилагане на филтъра върху Car
                List<Car> filteredCars = context.Cars.Where(carFilter).ToList();

                List<int> carIds = filteredCars.Select(car => car.Id).ToList(); // Вземаме само ID-тата на филтрираните коли

                List<Ad> filteredAds = context.Ads
                    .Where(ad => carIds.Contains(ad.CarId)) // Филтрираме обявите, чийто CarId е в списъка
                    .ToList();

                // Подготовка на модела за изгледа
                var model = new IndexVM
                {
                    UserItems = context.Users.ToList(),
                    CarItems = filteredCars.Any() ? filteredCars : new List<Car>(),
                    AdsItems = filteredAds.Any() ? filteredAds : new List<Ad>(),
                    CategoryItems = context.Categories.ToList(),
                    CategoryAdItems = context.CategoriesAds.ToList()
                };

                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (this.HttpContext.Session.GetString("loggedUserId") != null)
            {
                HttpContext.Session.Clear();
            }
          
            return View();
        }
        
        [HttpPost]
        [ModelStateFilter]
        public IActionResult Login(LoginVM model) 
        {
            if (!ModelState.IsValid)
            { 
				return View(model);
            }

            CarSellingPlatformDbContext context = new CarSellingPlatformDbContext();
            User? loggeduser = context.Users.Where(u => u.Username == model.Username 
                                                    && u.Password == model.Password).FirstOrDefault();

            if (loggeduser == null) 
            {
                ModelState.AddModelError("authError", "User not found");
                return View(model);
            }

            this.HttpContext.Session.SetString("loggedUserId", loggeduser.Id.ToString());

            return RedirectToAction("Index", "Home");
        }

		[HttpGet]
		public IActionResult CreateUser()
		{

			return View();
		}

        [HttpPost]
        [ModelStateFilter]
        public IActionResult CreateUser(CreateVM model)
        {
            BaseRepository<User> repo = new BaseRepository<User>();

            User user = new User();
            user.Username = model.Username;
            user.Password = model.Password;
            user.Email = model.Email;
            user.Phone = model.Phone;
            user.LName = model.LName;
            user.FName = model.FName;

            repo.Add(user);

            User? loggeduser = repo.FirstOrDefault(x => x.Id == user.Id);

            this.HttpContext.Session.SetString("loggedUserId", loggeduser.Id.ToString());

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            BaseRepository<User> repo = new BaseRepository<User>();
            User item = repo.FirstOrDefault(x => x.Id == id);

            EditVM model = new EditVM();
            model.Id = item.Id;
            model.Username = item.Username;
            model.Password = item.Password;
            model.FName = item.FName;
            model.LName = item.LName;
            model.Email = item.Email;
            model.Phone = item.Phone;

            return View(model);
        }

        [HttpPost]
        [ModelStateFilter]
        public IActionResult Edit(EditVM model)
        {
            BaseRepository<User> repo = new BaseRepository<User>();

            User user = repo.FirstOrDefault(x => x.Id == model.Id);

            user.Username = model.Username;
            user.Password = model.Password;
            user.Email = model.Email;
            user.Phone = model.Phone;
            user.LName = model.LName;
            user.FName = model.FName;

            repo.Update(user);

            return RedirectToAction("GoProfile", "Home");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            BaseRepository<User> repo = new BaseRepository<User>();

            User? user = repo.FirstOrDefault(x => x.Id == id);

            if (user != null)
                repo.Delete(user);

            HttpContext.Session.Clear();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [SessionAuthFilter]
        public IActionResult GoProfile()
        {
            CarSellingPlatformDbContext context = new CarSellingPlatformDbContext();

            var model = new IndexVM
            {
                UserItems = context.Users.ToList(),
                CarItems = context.Cars.ToList(),
                AdsItems = context.Ads.ToList(),
                CategoryItems = context.Categories.ToList(),
                CategoryAdItems = context.CategoriesAds.ToList()
            };

            return View(model);
        }
        public ActionResult GoSeeAd(int id)
        {
            CarSellingPlatformDbContext context = new CarSellingPlatformDbContext();

            Ad? searchedAd = context.Ads.FirstOrDefault(x => x.Id == id);
            CategoryAd? searchedCategoryAd = context.CategoriesAds.FirstOrDefault(x => x.AdId == id);
            Category? searchedCategory = context.Categories.FirstOrDefault(x => x.Id == searchedCategoryAd.CategoryId);
            User? searchedUser = context.Users.FirstOrDefault(x => x.Id == searchedAd.OwnerId);
            Car? searchedCar = context.Cars.FirstOrDefault(x => x.Id == searchedAd.CarId);

            var model = new GoSeeAdVM
            {
                UserModel = searchedUser,
                CarModel = searchedCar,
                AdModel = searchedAd,
                CategoryModel = searchedCategory
            };

            return View(model);
        }
    }
}
