using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaveYourMoney_MVC.BusinessLogic;
using SaveYourMoney_MVC.Models;
using SaveYourMoney_MVC.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SaveYourMoney_MVC.Controllers
{
    public class BudgetController : Controller
    {
        private readonly ICategoryManager CategoryManager;
        private readonly IBudgetManager BudgetManager;

        public BudgetController(IBudgetManager budgetManager, ICategoryManager categoryManager)
        {
            this.CategoryManager = categoryManager;
            this.BudgetManager = budgetManager;
        }

        private int GetCurrentUserId()
        {
            var userId = HttpContext.Session.GetInt32("CustomerId");

            if (userId.HasValue)
                return userId.Value;

            var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "CustomerId");
            if (customerIdClaim != null)
            {
                userId = Convert.ToInt32(customerIdClaim.Value);
                HttpContext.Session.SetInt32("CustomerId", userId.Value);
                return userId.Value;
            }

            // Handle the case where user id is not available
            throw new InvalidOperationException("User id not found.");
        }

        // GET: /<controller>/
        [Authorize]
        [HttpGet]
        public IActionResult Budgets()
        {
            try
            {
    
                var userId = GetCurrentUserId();

                var username = HttpContext.Session.GetString("Username");

                List<Budget> listOfBudgets = BudgetManager.GetBudgets(Convert.ToInt32(userId));
                //listOfBudgets.Add(new Budget() {BudgetId = 1, Amount = 325.552, CategoryId = 1, CustomerId = 2, Date = DateTime.Now, Description = "Test Entry"  });
                var listOfCategories = CategoryManager.GetCategories(userId);

                var budgetViewModel = new BudgetViewModel() {Budgets = listOfBudgets, Categories = listOfCategories };

                return View(budgetViewModel);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                ViewBag.ErrorMessage = error;
                return View("CustomError");
            }
   
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateANewBudget()
        {
            var createANewBudgetViewModel = new AddANewBudgetViewModel();
            var userId = GetCurrentUserId();
            createANewBudgetViewModel.Categories = CategoryManager.GetCategories(userId);

            return View(createANewBudgetViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateANewBudget(AddANewBudgetViewModel addANewBudgetViewModel)
        {
            try
            {
                var userId = GetCurrentUserId();

                AddNewlyCreatedBudget(addANewBudgetViewModel, userId);
                return RedirectToAction("Budgets");

            }
            catch (Exception ex)
            {
                var e = ex.Message;
                return View("Error");

            }


        }

        [Authorize]
        [HttpGet]
        public IActionResult EditBudget(int budgetId)
        {
            // Retrieve category details by categoryId
            var customerId = HttpContext.Session.GetInt32("CustomerId");
            var budget = BudgetManager.GetBudgetForACategory(customerId,budgetId);
            EditABudgetViewModel viewModel;
            if (budget == null)
            {
                // Budget not found, set error message and return Error view
                ViewData["ErrorMessage"] = $"Sorry, an error has occurred. Could not fetch the budget with ID: {budgetId}";
                ViewData["Errors"] = new List<string>();
                return View("Error");
            }
            else
            {
                var category = CategoryManager.GetCategoryByCategoryId(customerId, budget.CategoryId);
                viewModel = new EditABudgetViewModel
                {
                    // get budget name by budget Id
                    BudgetId = budget.BudgetId,
                    Amount = budget.Amount,
                    OldCategoryId = budget.CategoryId,
                    Categories = CategoryManager.GetCategories(Convert.ToInt32(customerId)),
                    CategoryName = category.CategoryName,
                    Description = budget.Description,
                    Date = budget.Date

                };
            }

            // Pass the category details to the view
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditBudget(EditABudgetViewModel editABudgetViewModel)
        {
            var customerId = HttpContext.Session.GetInt32("CustomerId");

            if (customerId == null)
            {
                ViewData["ErrorMessage"] = "Customer ID is not available. Please log in again.";
                return View("Error");
            }

            if (editABudgetViewModel.BudgetId <= 0)
            {
                ModelState.AddModelError(string.Empty, "Invalid budget ID.");
                return View(editABudgetViewModel); // Return the view if the BudgetId is invalid
            }

            if (editABudgetViewModel.NewCategoryId == 0)
            {
                // If NewCategoryId is not set, use OldCategoryId
                editABudgetViewModel.NewCategoryId = editABudgetViewModel.OldCategoryId;
            }

            try
            {
                BudgetManager.UpdateBudget(
                    customerId.Value, 
                    editABudgetViewModel.BudgetId, 
                    editABudgetViewModel.NewCategoryId, 
                    editABudgetViewModel.Amount, 
                    editABudgetViewModel.Description, 
                    editABudgetViewModel.Date
                );
            }
            catch (Exception ex)
            {
                var e = ex.Message;
                ModelState.AddModelError(string.Empty, "An error occurred while updating the budget: " + ex.Message);
                return View(editABudgetViewModel); // Return the view with the error message
            }

            return RedirectToAction("Budgets");
        }


        [Authorize]
        [HttpPost]
        public IActionResult DeleteBudget(int budgetId)
        {
            // Retrieve customer ID from session
            var customerId = HttpContext.Session.GetInt32("CustomerId");

            try
            {
                // Call a method to delete the budget from your data store
                BudgetManager.DeleteBudget(customerId, budgetId);

                // If the deletion is successful, you can redirect the user to a different page
                return RedirectToAction("Budgets");
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during deletion
                ViewData["ErrorMessage"] = $"An error occurred while deleting the budget: {ex.Message}";
                ViewData["Errors"] = new List<string>();
                return View("Error");
            }
        }


        private bool AddNewlyCreatedBudget(AddANewBudgetViewModel addANewBudgetViewModel, int userId)
        {
            try
            {
                BudgetManager.AddBudget(
              userId, addANewBudgetViewModel.SelectedCategoryId,
              addANewBudgetViewModel.Amount, addANewBudgetViewModel.Description, addANewBudgetViewModel.Date
              );
                return true;
            }
            catch (Exception ex)
            {
                var e = ex.Message;
            }
            return false;
        }

        [Authorize]
        [HttpGet]
        public IActionResult EditABudget()
        {
            var editABudgetViewModel = new EditABudgetViewModel();

            return View(editABudgetViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditABudget(EditABudgetViewModel editABudgetViewModel)
        {
            var submit =  editABudgetViewModel;

            return View(EditABudget());
        }

    }
}

