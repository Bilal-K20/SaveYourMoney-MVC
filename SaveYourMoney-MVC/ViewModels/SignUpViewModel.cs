using System;
using System.ComponentModel.DataAnnotations;

namespace SaveYourMoney_MVC.ViewModels
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage = "First name is required!")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Last name is required!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Valid email is required!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "A securePassword is required!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "You have to retype your password to confirm!")]
        public string ConfirmPassword { get; set; }
    }
}

