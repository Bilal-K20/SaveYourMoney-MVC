using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IActionResult ViewExpenses()
        {
            int userId = GetUserIdFromSession();
            UserId = userId;

            List<Expense> list = new List<Expense>();

            var expenses = ExpenseManager.GetAllExpenses(userId);
            var categories = CategoryManager.GetCategories(userId);


            list = (List<Expense>)ExpenseManager.GetAllExpenses(userId);



            var expenseViewModel = new ExpenseViewModel { Categories = categories, Expenses = expenses };



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
        public IActionResult AddExpense(AddAnExpenseViewModel model)
        {
            // Check model validity
            if (ModelState.IsValid)
            {
                // Your logic to save the expense
                // For example:
                // expenseService.AddExpense(model);

                // Redirect to another action after successful submission
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Model is invalid, return the partial view with validation errors
                // Repopulate Categories in case of redisplaying the form

                model.Categories = GetCategories(UserId);
                return View("ViewExpenses");
            }
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

