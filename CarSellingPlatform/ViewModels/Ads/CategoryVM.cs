using Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace CarSellingPlatform.ViewModels.Ads
{
    public class CategoryVM
    {
        [Required(ErrorMessage = "This filed is required!")]
        public CategoryType CategoryName { get; set; }
    }
}
