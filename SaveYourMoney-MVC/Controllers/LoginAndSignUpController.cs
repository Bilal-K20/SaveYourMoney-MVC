using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
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
        private readonly ISignUpManager _signUpManager;



        public LoginAndSignUpController(ILoginManager loginManager, ISignUpManager signUpManager)
        {
            _loginManager = loginManager;
            _signUpManager = signUpManager;
        }


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
            bool isValidUser = await IsValidUser(loginViewModel.Username, loginViewModel.Password);

            int? storedCustomerId;

            //insetad of calling this method I have tried to get the UserId when the signs in so if it is
            // successful then it will get the populate LoginResult
            //var userId = GetUserIdByUsername(loginViewModel.Username);

            if (isValidUser)
            {

                var loginResult = await _loginManager.LoginAsync(loginViewModel.Username, loginViewModel.Password);


                if (loginResult == null)
                {
                    // Log a message indicating that loginResult is null
                    _logger.LogError("Login result is null.");

                    // Return an error response
                    return Json(new { success = false, message = "An unexpected error occurred." });
                }
                else
                {
                    try
                    {
                        //Store user ID in session

                        HttpContext.Session.SetInt32("CustomerId", loginResult.UserId);

                        storedCustomerId = HttpContext.Session.GetInt32("CustomerId");


                        // Set the CustomerId cookie
                        Response.Cookies.Append("CustomerId", loginResult.UserId.ToString(), new CookieOptions
                        {
                            Expires = DateTimeOffset.Now.AddMinutes(30), // Set cookie expiration time
                            HttpOnly = true, // Make the cookie inaccessible to JavaScript
                            Secure = true // Ensure the cookie is only sent over HTTPS
                        });

                    }
                    catch (Exception ex)
                    {
                        // Log the exception for debugging
                        _logger.LogError(ex, "Error setting CustomerId in session");
                        // Optionally handle the exception or return an error response
                        return Json(new { success = false, message = "An unexpected error occurred." });
                    }
                }
            

                // Set authentication cookie
                var claims = new List<Claim>

                {

                    new Claim(ClaimTypes.Name, loginViewModel.Username),
                    new Claim("CustomerId", storedCustomerId.ToString())

                    // Add any additional claims as needed

                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    // Configure authentication properties (e.g., remember me)
                    AllowRefresh = true,
                    IsPersistent = loginViewModel.KeepMeLoggedIn
                   
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                // Return success response
                return Json(new { success = true, message = "Login Successful!", redirectTo = "/Dashboard/dashboard" });
            }
            else
            {
                // Return error response
                return Json(new { success = false, message = "Invalid username or password" });
            }
        }

        private int GetUserIdByUsername(string username)
        {
            var userId = -1;
            try
            {
                // MUST READ - possibly good to include in the report
                //just a thought is it worth just returning a whole object with all the details
                // maybe having objects in session might cause a lot of load on the server 
                var userDetails = _loginManager.GetUserDeatilsByUsername(username);
            }
            catch (Exception ex)
            {

            }

            return userId;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            var signUpViewModel = new SignUpViewModel();
            return View(signUpViewModel);
        }

        [HttpPost]
        public IActionResult SignUp(SignUpViewModel signUpViewModel)
        {
            int isUserRegistered = -1;

            var firstName = signUpViewModel.FirstName;
            var lastName = signUpViewModel.LastName;
            var email = signUpViewModel.Email;
            var username = signUpViewModel.Username;
            var password = signUpViewModel.Password;
            var confirmPassword = signUpViewModel.ConfirmPassword;

            var isSubmissionValidated = _signUpManager.checkUserSubmission(firstName, lastName, email, username, password, confirmPassword);

            if (isSubmissionValidated)
            {
                isUserRegistered = _signUpManager.RegisterANewCustomer(firstName, lastName, email, username, password);

            }

            if (isUserRegistered == -1)
            {

                return RedirectToAction("Login", "LoginAndSignUp");

            }
            else
            {
                return View(signUpViewModel);
            }

        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        private async Task<bool> IsValidUser(string username, string password)
        {
            bool isValidUser = false;
            try
            {
                //var loginDetails = _loginManager.LoginAsync(username, password);
                var loginResult = await _loginManager.LoginAsync(username, password);

                if (loginResult != null && loginResult.Success)
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