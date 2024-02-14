using System;
namespace SaveYourMoney_MVC.BusinessLogic
{
	public interface ISignUpManager
	{
		bool checkUserSubmission(string firstname, string lastname, string email, string username, string password, string confirmPassword);
		bool RegisterANewCustomer(string firstname, string lastname, string email, string username, string password);
	}
}

