using System;
namespace SaveYourMoney_MVC.ViewModels
{
	public class LoginViewModel
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public bool KeepMeLoggedIn { get; set; }
	}
}

