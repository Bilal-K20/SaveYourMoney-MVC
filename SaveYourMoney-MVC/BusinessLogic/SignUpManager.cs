using System;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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


        public int RegisterANewCustomer(string firstname, string lastname, string email, string username, string password)
        {
            bool isRegisterationSuccessful = false;

            try
            {
                if (isUserSubmissionValidated)
                {
                    //Need a method to check if the user qalready exists
                    //Linq would be quickets and easiest way
                    // the db currently allows users to register with exact same email etc
                    // need to look at how to change the column properties.

                    var newCustomer = new Customer();
                    newCustomer.FirstName = firstname;
                    newCustomer.LastName = lastname;
                    newCustomer.Email = email;
                    newCustomer.UserName = username;
                    newCustomer.Password = password;

                    _dbContext.Customers.Add(newCustomer);
                    _dbContext.SaveChanges();

                    return newCustomer.CustomerId;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured while registering a new customer: {ex.Message}");
            }
            
        
            return -1;
        }

        private async Task SignInAsync(Customer user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.CustomerId.ToString()), // Store CustomerId in claims
            new Claim(ClaimTypes.Name, user.UserName), // Store UserName in claims
            // Add more claims as needed
        };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                // Set any authentication properties if needed
            };

            //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
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

