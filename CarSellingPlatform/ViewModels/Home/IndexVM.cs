using Common.Entities;
using Common.Enums;

namespace CarSellingPlatform.ViewModels.Home
{
    public class IndexVM
    {
       public List<User> UserItems { get; set; }
       public List<Car> CarItems { get; set; }
       public  List<Ad> AdsItems { get; set; }
       public List<Category> CategoryItems { get; set; }
       public List<CategoryAd> CategoryAdItems { get; set; }
        public BrandType? Brand { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public FuelType? Engine { get; set; }
        public double? MinMileage { get; set; }
        public double? MaxMileage { get; set; }
        public int? MinHorsePower { get; set; }
        public int? MaxHorsePower { get; set; }
    }
}
