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
        private readonly IBudgetManager BudgetManager;

        private int? customerIdInt;

        public CategoryController(ICategoryManager categoryManager, IBudgetManager budgetManager)
        {
            CategoryManager = categoryManager;
            BudgetManager = budgetManager;
        }


        // GET: /<controller>/
        [Authorize]
        [HttpGet]
        public IActionResult Category()
        {
            var categoryViewModel = new CategoryViewModel();

            string customerId;

            // Example of retrieving CustomerId from claims
            var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "CustomerId");
            if (customerIdClaim != null)
            {
                customerId = customerIdClaim.Value;
                HttpContext.Session.SetInt32("CustomerId", Convert.ToInt32(customerIdClaim.Value));

                // Use customerId as needed
            }

            var userId = HttpContext.Session.GetInt32("CustomerId");
            customerIdInt = Convert.ToInt32(userId);

            var username = HttpContext.Session.GetString("Username");


            var listOfCategories = CategoryManager.GetCategories(Convert.ToInt32(userId));

            var listOfBudgets = BudgetManager.GetBudgets(Convert.ToInt32(userId));


            categoryViewModel.Categories = listOfCategories;
            categoryViewModel.Budgets = listOfBudgets;



            return View(categoryViewModel);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddCategory()
        {
            var addCategoryViewModel = new AddCategoryViewModel();
            return View(addCategoryViewModel);
        }


        [Authorize]
        [HttpPost]
        public IActionResult AddCategory(AddCategoryViewModel addCategoryViewModel)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(addCategoryViewModel.CategoryName))
            {
                // Handle invalid input
                return RedirectToAction("Categories", new { error = "Category name cannot be empty" });
            }

            //if (customerId == null)
            //{

            //}

            // Create the category entity
            var categoryName = addCategoryViewModel.CategoryName;
            var customerId = HttpContext.Session.GetInt32("CustomerId");

            // Add category to the database
            CategoryManager.AddCategory(customerId, categoryName);

            // Redirect back to the Categories page
            return RedirectToAction("Category");
        }


    }
}

