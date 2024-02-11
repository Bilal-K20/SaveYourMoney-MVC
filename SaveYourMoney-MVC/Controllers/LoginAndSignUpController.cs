using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SaveYourMoney_MVC.BusinessLogic;
using SaveYourMoney_MVC.Repositories;
using SaveYourMoney_MVC.ViewModels;

namespace SaveYourMoney_MVC.Controllers
{
    public class LoginAndSignUpController : Controller
    {
        private readonly AppDbContext _context;

        private readonly ILogger<LoginAndSignUpController> _logger;
        private readonly ILoginAndSignUpManager LoginAndSignUpManager;
        private readonly ILoginManager _loginManager;
        //private readonly ISaveYourMoneyDatabase saveYourMoneyDatabase;
        //private readonly ISaveYourMoneyDbContext SaveYourMoneyDbContext;

        //public LoginAndSignUpController(ILogger<HomeController> logger)
        //{
        //    _logger = (ILogger<LoginAndSignUpController>?)logger;
        //}


        //public LoginAndSignUpController(ISaveYourMoneyDbContext saveYourMoneyDbContext)
        //{
        //    SaveYourMoneyDbContext = saveYourMoneyDbContext;
        //}

        public LoginAndSignUpController(ILoginManager loginManager)
        {
            _loginManager = loginManager;
        }
        //public LoginAndSignUpController(ILoginAndSignUpManager loginAndSignUpManager)
        //{
        //    LoginAndSignUpManager = loginAndSignUpManager;
        //}

        [HttpGet]
        public IActionResult Login()
        {
            var loginViewModel = new LoginViewModel();
            return View(loginViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {

            // Validate the user's credentials (e.g., using a service or database query)
            if (IsValidUser(loginViewModel.Username, loginViewModel.Password))
            {
                // Set authentication cookie
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, loginViewModel.Username)
                        // Add any additional claims as needed
                    };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    // Configure authentication properties (e.g., remember me)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return Json(new { success = true, redirectTo = "/Dashboard/dashboard" }); // Redirect to the desired page after login


                //return RedirectToAction("Index", "Home"); // Redirect to the desired page after login
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
            }

            return View(loginViewModel);
        }

        private bool IsValidUser(string username, string password)
        {
            bool isValidUser = false;
            try
            {
                isValidUser = LoginAndSignUpManager.GetUserLoginDetails(username);

            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }



            // Check if the username meets the required criteria (at least 3 characters and not an email)
            //var usernameRegex = new Regex(@"^(?!.*@).{3,}$");
            //if (!usernameRegex.IsMatch(username))
            //{
            //    return false;
            //}
            //else if (username == usernameFromDb && password == passwordFromDb)
            //{
            //    return true;
            //}


            // Here you would typically validate the password against some stored hash or external service
            // For demonstration purposes, let's assume the password is valid if it's not empty
            //if (string.IsNullOrEmpty(password))
            //{
            //    return false;
            //}

            // If both username and password pass validation, return true
            return isValidUser;
        }
    }
}