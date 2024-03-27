using System;
namespace SaveYourMoney_MVC.BusinessLogic
{
	public interface ISignUpManager
	{
		bool checkUserSubmission(string firstname, string lastname, string email, string username, string password, string confirmPassword);
		int RegisterANewCustomer(string firstname, string lastname, string email, string username, string password);
		List<string> Guard(string firstname, string lastname, string email, string username, string password, string confirmPassword);
	}
}

