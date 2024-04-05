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
            var customerIdInt = Convert.ToInt32(userId);

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

            // Create the category entity
            var categoryName = addCategoryViewModel.CategoryName;
            var customerId = HttpContext.Session.GetInt32("CustomerId");

            // Add category to the database
            CategoryManager.AddCategory(customerId, categoryName);

            // Redirect back to the Categories page
            return RedirectToAction("Category");
        }

        [Authorize]
        [HttpGet]
        public IActionResult EditCategory(int categoryId)
        {
            // Retrieve category details by categoryId
           var customerId = HttpContext.Session.GetInt32("CustomerId");
            var category = CategoryManager.GetCategoryByCategoryId(customerId,categoryId);
            EditCategoryViewModel viewModel;
            if (category == null)
            {
                // Category not found, return not found error
                return NotFound();
            }
            else
            {
                viewModel = new EditCategoryViewModel
                {
                    CategoryId = categoryId ,
                    NewCategoryName = category.CategoryName,
                    OldCategoryName = category.CategoryName
                };
            }

            // Pass the category details to the view
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditCategory(EditCategoryViewModel editCategoryViewModel)
        {

            var customerId = HttpContext.Session.GetInt32("CustomerId");

            if (!ModelState.IsValid)
            {
                return View(editCategoryViewModel); // Return the view with validation errors
            }


            // Check if the new category name is different from the old category name
            if (editCategoryViewModel.OldCategoryName == editCategoryViewModel.NewCategoryName)
            {
                ModelState.AddModelError("NewCategoryName", "New category name must be different from the old category name.");
                return View(editCategoryViewModel); // Return the view with validation error for the new category name
            }

            // Update the category name
            CategoryManager.UpdateCategory(customerId, editCategoryViewModel.CategoryId, editCategoryViewModel.NewCategoryName);

            return RedirectToAction("Category");
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteCategory(int categoryId)
        {
            var customerId = HttpContext.Session.GetInt32("CustomerId");

            // Retrieve category details by categoryId
            var category = CategoryManager.GetCategoryByCategoryId(customerId,categoryId);

            if (category == null)
            {
                // Category not found, return not found error
                return NotFound();
            }

            // Delete the category from the database
            CategoryManager.DeleteCategory(customerId,categoryId);

            // Redirect back to the Categories page
            return RedirectToAction("Category");
        }



    }
}

