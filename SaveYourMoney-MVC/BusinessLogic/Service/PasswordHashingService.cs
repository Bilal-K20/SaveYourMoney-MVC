using System;
using Microsoft.CodeAnalysis.Scripting;
using BCrypt;

namespace SaveYourMoney_MVC.BusinessLogic.Service
{
    public class PasswordHashingService : IPasswordHashingService
    {
        // Hash a password using bcrypt
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Verify a password against a hashed password
        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }

}

