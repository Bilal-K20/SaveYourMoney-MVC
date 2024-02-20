using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SaveYourMoney_MVC.Repositories;

namespace SaveYourMoney_MVC.BusinessLogic
{

    public class LoginManager : ILoginManager
    {
        private readonly AppDbContext _dbContext;

        public LoginManager(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetUserDeatilsByUsername(string username)
        {
            var userId = -1;

            try
            {
                var user = await _dbContext.Customers.FirstOrDefaultAsync(u => u.UserName == username);

                if (user == null)
                {
                }
                else
                {
                    userId = user.CustomerId;
                }
            }
            catch (Exception ex)
            {
                var e = ex.Message;
            }
            return userId;
        }

        /*
         * MAYBE I COULD RETURN USER ID ON SUCCESSFUL LOGIN... 
         */
        public async Task<LoginResult> LoginAsync(string username, string password)
        {
            int userId = -1;
            try
            {


               userId = await GetUserDeatilsByUsername(username);

                var user = await _dbContext.Customers.FirstOrDefaultAsync(u => u.UserName == username);

                if (user == null)
                {
                    return new LoginResult { Success = false, Message = "User not found." };
                }

                if (!VerifyPassword(password, user.Password))
                {
                    return new LoginResult { Success = false, Message = "Incorrect password." , UserId = user.CustomerId};
                }

            }
            catch (Exception ex)
            {
                 var errorMessage = ex.Message;
                return new LoginResult { Success = false, Message = "Sorry something went horribly wrong..." };

            }
            return new LoginResult { Success = true, Message = "Login successful.", UserId = userId };

        }

        // You need to implement this method based on how you store and verify passwords
        private bool VerifyPassword(string password, string passwordHash)
        {
            // Implement password verification logic here
            // For simplicity, let's assume we're comparing plain passwords for now
            return password == passwordHash;
        }
    }

}

