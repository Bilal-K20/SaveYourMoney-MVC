using System;
using System.Threading.Tasks;

namespace SaveYourMoney_MVC.BusinessLogic
{
	public interface ILoginManager
	{
        Task<LoginResult> LoginAsync(string username, string password);

    }
}

