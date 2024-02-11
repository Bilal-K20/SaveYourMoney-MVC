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

        public async Task<LoginResult> LoginAsync(string username, string password)
        {
            var user = await _dbContext.Customers.FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                return new LoginResult { Success = false, Message = "User not found." };
            }

            if (!VerifyPassword(password, user.Password))
            {
                return new LoginResult { Success = false, Message = "Incorrect password." };
            }

            return new LoginResult { Success = true, Message = "Login successful." };
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

