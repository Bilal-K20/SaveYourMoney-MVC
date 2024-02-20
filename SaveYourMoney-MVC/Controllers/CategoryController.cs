using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaveYourMoney_MVC.BusinessLogic;
using SaveYourMoney_MVC.Models;
using SaveYourMoney_MVC.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SaveYourMoney_MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryManager CategoryManager;

        public CategoryController(ICategoryManager categoryManager)
        {
            CategoryManager = categoryManager;
        }


        // GET: /<controller>/
        [Authorize]
        [HttpGet]
        public IActionResult Category()
        {
            var categoryViewModel = new CategoryViewModel();

            var listOfCategories = CategoryManager.GetCategories();

            categoryViewModel.Categories = listOfCategories;


            //int customerId = -1; // Default value if CustomerId is not found in the session

            // Check if the "CustomerId" key exists in the session

            string customerId;

            // Example of retrieving CustomerId from claims
            var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "CustomerId");
            if (customerIdClaim != null)
            {
                 customerId = customerIdClaim.Value;
                // Use customerId as needed
            }

            var userId = HttpContext.Session.GetInt32("CustomerId");
            var username = HttpContext.Session.GetString("Username");

            //if (HttpContext.Session.Keys.Contains("CustomerId"))
            //{
            //    // The key exists, retrieve its value
            //    byte[] customerIdBytes;
            //    if (HttpContext.Session.TryGetValue("CustomerId", out customerIdBytes))
            //    {
            //        // Convert the byte array to int
            //        customerId = BitConverter.ToInt32(customerIdBytes);
            //    }
            //    else
            //    {
            //        // Handle the case where CustomerId value is null
            //        // This could occur if the value associated with the key is null
            //        // For example:
            //        // return BadRequest("CustomerId value is null.");
            //    }
            //}
            //else
            //{
            //    // Handle the case where CustomerId key is not found in the session
            //    // For example:
            //    // return BadRequest("CustomerId key not found in the session.");
            //}

            return View(categoryViewModel);
        }

        [HttpPost]
        public IActionResult AddCategory(AddCategoryViewModel addCategoryViewModel)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(addCategoryViewModel.CategoryName))
            {
                // Handle invalid input
                return RedirectToAction("Categories", new { error = "Category name cannot be empty" });
            }

            // Create the category entity
            var category = new Category { CategoryName = addCategoryViewModel.CategoryName };

            // Add category to the database
            CategoryManager.AddCategory(category);

            // Redirect back to the Categories page
            return RedirectToAction("Categories");
        }


    }
}

