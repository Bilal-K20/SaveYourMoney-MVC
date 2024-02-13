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
            bool isValidUser = IsValidUser(loginViewModel.Username, loginViewModel.Password);

            if (isValidUser)
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

                // Return success response
                return Json(new { success = true, message = "Login Successful!" , redirectTo = "/Dashboard/dashboard" });
            }
            else
            {
                // Return error response
                return Json(new { success = false, message = "Invalid username or password" });
            }
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            var signUpViewModel = new SignUpViewModel();
            return View(signUpViewModel);
        }


        public IActionResult AccessDenied()
        {
            return View();
        }

        private bool IsValidUser(string username, string password)
        {
            bool isValidUser = false;
            try
            {
                var loginDetails = _loginManager.LoginAsync(username, password);
                if (loginDetails != null && loginDetails.Result.Success)
                {
                    isValidUser = true;
                }
                else
                {
                    isValidUser = false;
                }

            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

            return isValidUser;
        }
    }
}