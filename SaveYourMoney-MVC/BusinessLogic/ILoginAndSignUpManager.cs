using System;
using SaveYourMoney_MVC.Entities;

namespace SaveYourMoney_MVC.BusinessLogic
{
	public interface ILoginAndSignUpManager
	{
        bool GetUserLoginDetails(string userEnteredUsername);
    }
}

