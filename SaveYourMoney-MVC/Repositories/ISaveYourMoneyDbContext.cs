using System;
using SaveYourMoney_MVC.Entities;

namespace SaveYourMoney_MVC.Repositories
{
	public interface ISaveYourMoneyDbContext
	{
        UserLoginDetails GetUserLoginDetails(string userEnteredUsername);

    }
}

