using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarSellingPlatform.ViewModels.Home
{
	public class LoginVM
	{
		[Required(ErrorMessage = "This filed is required!")]
		public string Username { get; set; }

		[Required(ErrorMessage = "This filed is required!")]
		public string Password { get; set; }
	}
}
