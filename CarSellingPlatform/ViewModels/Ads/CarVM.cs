using Common.Entities;
using Common.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CarSellingPlatform.ViewModels.Ads
{
    public class CarVM
    {
        [Required(ErrorMessage = "This filed is required!")]
        public int OwnerId { get; set; }

        [Required(ErrorMessage = "This filed is required!")]
        public BrandType Brand { get; set; }

        [Required(ErrorMessage = "This filed is required!")]
        public string Model { get; set; }

        [Required(ErrorMessage = "This filed is required!")]
        public string Year { get; set; }

        [Required(ErrorMessage = "This filed is required!")]
        public double Price { get; set; }

        [Required(ErrorMessage = "This filed is required!")]
        public FuelType Engine { get; set; }

        [Required(ErrorMessage = "This filed is required!")]
        public double MileageInKm { get; set; }

        [Required(ErrorMessage = "This filed is required!")]
        public int HorsePower { get; set; }
    }
}
