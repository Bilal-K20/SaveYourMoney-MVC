using System;
using SaveYourMoney_MVC.Entities;

namespace SaveYourMoney_MVC.Repositories
{
	public interface ISaveYourMoneyDatabase
	{
        UserLoginDetails GetUserLoginDetails(string userEnteredUsername);

    }
}

