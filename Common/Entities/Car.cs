using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public enum Fuel 
    { 
        Petrol,
        Diesel,
        Gas
    }

    public enum BrandType
    {
        Acura,
        Alfa_Romeo,
        Aston_Martin,
        Audi,
        Bentley,
        BMW,
        Bugatti,
        Buick,
        Cadillac,
        Chevrolet,
        Chrysler,
        Citroën,
        Dacia,
        Daewoo,
        Daihatsu,
        Dodge,
        Ferrari,
        Fiat,
        Ford,
        Geely,
        Genesis,
        GMC,
        Honda,
        Hyundai,
        Hummer,
        Infiniti,
        Isuzu,
        Iveco,
        Jaguar,
        Jeep,
        Kia,
        Lamborghini,
        Lancia,
        Land_Rover,
        Lexus,
        Lincoln,
        Lotus,
        Maserati,
        Mazda,
        McLaren,
        Mercedes_Benz,
        Mini,
        Mitsubishi,
        Nissan,
        Opel,
        Peugeot,
        Polestar,
        Porsche,
        RAM,
        Renault,
        Rolls_Royce,
        Saab,
        Seat,
        Škoda,
        Smart,
        Subaru,
        Suzuki,
        Tata,
        Tesla,
        Toyota,
        Volkswagen,
        Volvo,
        Zotye
    }
    public class Car : BaseEntity
    {
        public int OwnerId { get; set; }
        public BrandType Brand { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public double Price { get; set; }
        public Fuel Engine { get; set; }
        public double MileageInKm { get; set; }
        public int HorsePower { get; set; }
        public User Owner { get; set; }
    }
}
