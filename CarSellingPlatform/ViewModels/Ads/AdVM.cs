using Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace CarSellingPlatform.ViewModels.Ads
{
    public class AdVM
    {
        [Required(ErrorMessage = "This filed is required!")]
        public int OwnerId { get; set; }

        [Required(ErrorMessage = "This filed is required!")]
        public int CarId { get; set; }

        [Required(ErrorMessage = "This filed is required!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This filed is required!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "This filed is required!")]
        public string Place{ get; set; }

        [Required(ErrorMessage = "This filed is required!")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "This filed is required!")]
        public IFormFile Image { get; set; } // Поле за качване на файла
        public string ImagePath { get; set; }

    }
}
