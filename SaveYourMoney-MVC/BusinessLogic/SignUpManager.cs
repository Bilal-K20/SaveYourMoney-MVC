using System;
using System.Text.RegularExpressions;
using SaveYourMoney_MVC.Models;
using SaveYourMoney_MVC.Repositories;

namespace SaveYourMoney_MVC.BusinessLogic
{
    public class SignUpManager : ISignUpManager
    {
        private readonly AppDbContext _dbContext;

        public SignUpManager(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        bool isUserSubmissionValidated;

        public bool checkUserSubmission(string firstname, string lastname, string email, string username, string password, string confirmPassword)
        {
            isUserSubmissionValidated =  ValidateAllUserSubmission(firstname, lastname, email, username, password, confirmPassword);
            return isUserSubmissionValidated;
        }


        public bool RegisterANewCustomer(string firstname, string lastname, string email, string username, string password)
        {
            bool isRegisterationSuccessful = false;

            if (isUserSubmissionValidated)
            {
                var newCustomer = new Customer();
                newCustomer.FirstName = firstname;
                newCustomer.LastName = lastname;
                newCustomer.Email = email;
                newCustomer.UserName = username;
                newCustomer.Password = password;

                _dbContext.Customers.Add(newCustomer);
                _dbContext.SaveChanges();

                isRegisterationSuccessful = true;

            }
        
            return isRegisterationSuccessful;
        }
        private bool ValidateAllUserSubmission(string firstname, string lastname, string email, string username, string password, string confirmPassword)
        {
            // Validate first and last name
            if (!IsValidName(firstname) || !IsValidName(lastname))
                return false;

            // Validate email
            if (!IsValidEmail(email))
                return false;

            // Validate username
            if (!IsValidUsername(username))
                return false;

            // Validate password
            if (!IsValidPassword(password, confirmPassword))
                return false;

            // All inputs are valid
            return true;
        }

        private bool IsValidName(string name)
        {
            // Check if name is at least 3 characters long and contains only letters
            return name.Length >= 3 && Regex.IsMatch(name, @"^[a-zA-Z]+$");
        }

        private bool IsValidEmail(string email)
        {
            // Use regular expression to validate email format
            return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        }

        private bool IsValidUsername(string username)
        {
            // Check if username contains only letters, numbers, and underscores
            return Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$");
        }

        private bool IsValidPassword(string password, string confirmPassword)
        {
            // Check if password is at least 6 characters long and contains at least one special character
            if (password.Length < 6)
            {
                return false;

            }else if (password != confirmPassword)
            {
                // Check if password matches confirm password

                return false;
            }

            // Use regular expression to check for special character
            return Regex.IsMatch(password, @"[^a-zA-Z0-9]");
        }
    }


}

