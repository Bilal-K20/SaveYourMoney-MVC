using System;
using System.Collections.Generic;
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

