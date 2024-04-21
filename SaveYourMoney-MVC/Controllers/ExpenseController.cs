using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaveYourMoney_MVC.BusinessLogic;
using SaveYourMoney_MVC.Models;
using SaveYourMoney_MVC.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SaveYourMoney_MVC.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ICategoryManager CategoryManager;
        private readonly IBudgetManager BudgetManager;
        private readonly IExpenseManager ExpenseManager;
        public int UserId { get; set; }

        public ExpenseController(ICategoryManager categoryManager, IBudgetManager budgetManager, IExpenseManager expenseManager)
        {
            this.CategoryManager = categoryManager;
            this.BudgetManager = budgetManager;
            this.ExpenseManager = expenseManager;
        }
        // GET: /<controller>/
        [Authorize]
        [HttpGet]
        public IActionResult ViewExpenses()
        {
            int userId = GetUserIdFromSession();
            UserId = userId;

            List<Expense> list = new List<Expense>();

            var expenses = ExpenseManager.GetAllExpenses(userId);
            var categories = CategoryManager.GetCategories(userId);
            var months = ExpenseManager.GetMonthsList();


            list = (List<Expense>)ExpenseManager.GetAllExpenses(userId);



            var expenseViewModel = new ExpenseViewModel { Categories = categories, Expenses = expenses, Months = months};



            return View(expenseViewModel);
        }

        private int GetUserIdFromSession()
        {
            string customerId;

            //get user Id from the session
            var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "CustomerId");
            if (customerIdClaim != null)
            {
                customerId = customerIdClaim.Value;
                HttpContext.Session.SetInt32("CustomerId", Convert.ToInt32(customerIdClaim.Value));

                // Use customerId as needed
            }
            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("CustomerId"));
            return userId;
        }

        // GET: Expense/AddExpense
        [Authorize]
        [HttpGet]
        public IActionResult AddExpense()
        {
            var userId = GetUserIdFromSession();

            var viewModel = new AddAnExpenseViewModel();

            var addExpenseViewModel = new AddAnExpenseViewModel(); // Assuming you have a constructor or method to initialize the model
            // Populate Categories here
            addExpenseViewModel.Categories = GetCategories(userId); // You would replace this with your actual data retrieval logic
            //return View(viewModel);
            return PartialView("_AddAnExpensePartial", addExpenseViewModel);

        }

        // POST: Expense/AddExpense
        [Authorize]
        [HttpPost]
        //might need to use IFormFile to handle the document attached
        public IActionResult AddExpense(AddAnExpenseViewModel model)
        {
            // Check model validity 
            /*
             * Model state is valid always fails so might need to ignore this. 
             */

            var userId = GetUserIdFromSession();

            if (!string.IsNullOrWhiteSpace(model.ExpenseTitle) && model.ExpenseType != null && model.Date != null )
            {

            var check = ExpenseManager.IsNeccessaryDataPresent(model);
                ExpenseManager.AddExpense(userId, model);

            ViewBag.SuccessMessage = "Expense has been successfully created.";

            // Redirect to another action after successful submission
            return RedirectToAction("ViewExpenses", "Expense");
            }
            else
            {
                // Model is invalid, return the partial view with validation errors
                // Repopulate Categories in case of redisplaying the form

                model.Categories = GetCategories(UserId);


                return RedirectToAction("ViewExpenses");
            }
        }

        public IActionResult FilterExpenses(string date, string type, int? year, string? month)
        {
            var model = ExpenseManager.FilterExpenses(date, type, year, month);

            return PartialView("_ExpenseTablePartial", model);
        }

        // Dummy method to simulate category retrieval
        private List<SelectListItem> GetCategories(int userId)
        {
            List<SelectListItem> selectlistItem = new List<SelectListItem>(); 
            // Your logic to retrieve categories
            var categories = CategoryManager.GetCategories(userId);
            foreach (var category in categories)
            {
                selectlistItem.Add(new SelectListItem { Value = category.CategoryId.ToString(), Text = category.CategoryName });
            }
            return selectlistItem;
     
        }

  
    }
}

