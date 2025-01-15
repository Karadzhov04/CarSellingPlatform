using Common.Entities;

namespace CarSellingPlatform.ViewModels.Home
{
    public class GoProfileVM
    {
        public List<User> UserItems { get; set; }
        public List<Car> CarItems { get; set; }
        public List<Ad> AdsItems { get; set; }
    }
}
