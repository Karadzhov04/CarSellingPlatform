using Common.Entities;

namespace CarSellingPlatform.ViewModels.Home
{
    public class GoSeeAdVM
    {
        public Ad AdModel { get; set; }
        public Category CategoryModel { get; set; }

        public User UserModel { get; set; }
        public Car CarModel { get; set; }

    }
}
