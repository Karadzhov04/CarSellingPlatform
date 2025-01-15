using System.ComponentModel.DataAnnotations;

namespace CarSellingPlatform.ViewModels.Home
{
    public class CreateVM
    {
        [Required(ErrorMessage = "This filed is required!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "This filed is required!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This filed is required!")]
        public string FName { get; set; }

        [Required(ErrorMessage = "This filed is required!")]
        public string LName { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "This filed is required!")]
        public string Phone { get; set; }
    }
}
