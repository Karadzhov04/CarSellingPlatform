using CarSellingPlatform.ActionFilters;
using CarSellingPlatform.ImageProcess;
using CarSellingPlatform.ViewModels.Ads;
using CarSellingPlatform.ViewModels.Home;
using Common.Entities;
using Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Security.Claims;

namespace CarSellingPlatform.Controllers
{
    [SessionAuthFilter]
    public class AdsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateCar()
        {
            return View();
        }

        [HttpPost]
        [ModelStateFilter]
        public IActionResult CreateCar(CarVM car)
        {
            BaseRepository<Car> repo = new BaseRepository<Car>();

            Car item = new Car();
            item.Brand = car.Brand;
            item.Model = car.Model;
            item.Year = car.Year;
            var userId = HttpContext.Session.GetString("loggedUserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Home");
            }

            item.OwnerId = int.Parse(userId);

            item.Engine = car.Engine;
            item.Price = car.Price;
            item.MileageInKm = car.MileageInKm;
            item.HorsePower = car.HorsePower;

            repo.Add(item);

            return RedirectToAction("CreateAd", "Ads", new { id = item.Id });
        }

        [HttpGet]
        public IActionResult CreateAd(int id)
        {
            this.HttpContext.Session.SetString("CarId", id.ToString());

            return View();
        }

        [HttpPost]
        public IActionResult CreateAd(AdVM ad)
        {
            BaseRepository<Ad> repo = new BaseRepository<Ad>();
            BaseRepository<Car> repo2 = new BaseRepository<Car>();

            int id = int.Parse(this.HttpContext.Session.GetString("CarId"));

            Car? car = repo2.FirstOrDefault(x => x.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            if (ad.Image != null && ad.Image.Length > 0)
            {
                ad.ImagePath = PhotoProcess.CreateImage(ad.Image);
            }

            Ad item = new Ad();
            item.OwnerId = car.OwnerId;
            item.CarId = car.Id;
            item.Title = ad.Title;
            item.Description = ad.Description;
            item.Place = ad.Place;  
            item.CreatedAt = ad.CreatedAt;
            item.ImagePath = ad.ImagePath;

            repo.Add(item);

            int adId = item.Id;

            return RedirectToAction("ChooseCategory", "Ads", new { id = adId });
        }

        [HttpGet]
        public IActionResult ChooseCategory(int id)
        {
            this.HttpContext.Session.SetString("AdId", id.ToString());

            return View();
        }

        [HttpPost]
        [ModelStateFilter]
        public IActionResult ChooseCategory(CategoryVM categoryModel)
        {
            BaseRepository<Category> repo = new BaseRepository<Category>();
            BaseRepository<CategoryAd> repo2 = new BaseRepository<CategoryAd>();

            Category item = new Category();
            item.CategoryName = categoryModel.CategoryName;

            repo.Add(item);

            int id = int.Parse(this.HttpContext.Session.GetString("AdId"));

            CategoryAd adCategory = new CategoryAd();
            adCategory.CategoryId = item.Id;
            adCategory.AdId = id;

            repo2.Add(adCategory);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult EditCar(int id)
        {
            BaseRepository<Car> repo = new BaseRepository<Car>();
            Car item = repo.FirstOrDefault(x => x.Id == id);

            EditCarVM model = new EditCarVM();
            model.Brand = item.Brand;
            model.Model = item.Model;
            model.Year = item.Year;
            model.Price = item.Price;
            model.Engine = item.Engine;
            model.MileageInKm = item.MileageInKm;
            model.HorsePower = item.HorsePower;

            return View(model);
        }

        [HttpPost]
        [ModelStateFilter]
        public IActionResult EditCar(EditCarVM car)
        {
            BaseRepository<Car> repo = new BaseRepository<Car>();
            Car item = repo.FirstOrDefault(x => x.Id == car.Id);

            item.Brand = car.Brand;
            item.Model = car.Model;
            item.Year = car.Year;
            item.Price = car.Price;
            item.Engine = car.Engine;
            item.MileageInKm = car.MileageInKm;
            item.HorsePower = car.HorsePower;

            repo.Update(item);

            return RedirectToAction("GoProfile", "Home");
        }

        [HttpGet]
        public IActionResult EditAd(int id)
        {
            BaseRepository<Ad> repo = new BaseRepository<Ad>();
            Ad item = repo.FirstOrDefault(x => x.Id == id);

            EditAdVM model = new EditAdVM();
            model.Title = item.Title;
            model.Description = item.Description;
            model.ImagePath = item.ImagePath;
            model.CreatedAt = item.CreatedAt;

            return View(model);
        }

        [HttpPost]
        public IActionResult EditAd(EditAdVM model, string CurrentCreatedAt)
        {
            BaseRepository<Ad> repo = new BaseRepository<Ad>();
            Ad item = repo.FirstOrDefault(x => x.Id == model.Id);

            if (model.Image != null && model.Image.Length > 0)
            {
                model.ImagePath = PhotoProcess.CreateImage(model.Image);
            }

            if (model.CreatedAt == DateTime.MinValue)
            {
                model.CreatedAt = DateTime.Parse(CurrentCreatedAt);
            }

            item.Title = model.Title;
            item.Description = model.Description;
            item.CreatedAt = model.CreatedAt;
            item.ImagePath = model.ImagePath;

            repo.Update(item);

            return RedirectToAction("GoProfile", "Home");
        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            BaseRepository<Category> repo = new BaseRepository<Category>();
            Category item = repo.FirstOrDefault(x => x.Id == id);

            EditCategoryVM model = new EditCategoryVM();
            model.CategoryName = item.CategoryName;

            return View(model);
        }

        [HttpPost]
        [ModelStateFilter]
        public IActionResult EditCategory(EditCategoryVM model)
        {
            BaseRepository<Category> repo = new BaseRepository<Category>();
            Category item = repo.FirstOrDefault(x => x.Id == model.Id);

            item.CategoryName = model.CategoryName;

            repo.Update(item);

            return RedirectToAction("GoProfile", "Home");
        }

        public IActionResult DeleteAd(int id)
        {
            CarSellingPlatformDbContext context = new CarSellingPlatformDbContext();

            CategoryAd? searchedCategoryAd = context.CategoriesAds.Where(x => x.AdId == id).FirstOrDefault();
            if (searchedCategoryAd != null)
                context.CategoriesAds.Remove(searchedCategoryAd);

            Category? searchedCategory = context.Categories.FirstOrDefault(x => x.Id == searchedCategoryAd.CategoryId);
            if (searchedCategory != null)
                context.Categories.Remove(searchedCategory);

            Ad? searchedAd = context.Ads.FirstOrDefault(x => x.Id == id);
            if (searchedAd != null)
                context.Ads.Remove(searchedAd);
            context.SaveChanges();

            return RedirectToAction("GoProfile", "Home");
        }
    }
}
