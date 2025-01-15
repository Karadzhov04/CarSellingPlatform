﻿using Common.Entities;

namespace CarSellingPlatform.ViewModels.Ads
{
    public class CarFilterVM
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public Fuel? Engine { get; set; }
        public double? MinMileage { get; set; }
        public double? MaxMileage { get; set; }
        public int? MinHorsePower { get; set; }
        public int? MaxHorsePower { get; set; }

    }
}
