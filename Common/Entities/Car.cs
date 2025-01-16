using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class Car : BaseEntity
    {
        public int OwnerId { get; set; }
        public BrandType Brand { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public double Price { get; set; }
        public FuelType Engine { get; set; }
        public double MileageInKm { get; set; }
        public int HorsePower { get; set; }
        public User Owner { get; set; }
    }
}
