using System;
using System.Threading.Tasks;

namespace SaveYourMoney_MVC.BusinessLogic
{
	public interface ILoginManager
	{
        Task <int> GetUserDeatilsByUsername(string username);
        Task<LoginResult> LoginAsync(string username, string password);

    }
}

