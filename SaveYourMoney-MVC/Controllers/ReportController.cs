using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaveYourMoney_MVC.BusinessLogic;
using SaveYourMoney_MVC.Models;
using SaveYourMoney_MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SaveYourMoney_MVC.Controllers
{
    public class ReportController : Controller
    {
        private readonly IExpenseManager _expenseManager;
        private readonly ICategoryManager _categoryManager;
        private readonly IBudgetManager _budgetManager;
        private int _userId;

        public ReportController(IExpenseManager expenseManager, ICategoryManager categoryManager, IBudgetManager budgetManager)
        {
            _expenseManager = expenseManager;
            _categoryManager = categoryManager;
            _budgetManager = budgetManager;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index(int? year)
        {
            _userId = GetUserIdFromSession();
            var expenses = _expenseManager.GetAllExpenses(_userId);
            var categories = _categoryManager.GetCategories(_userId);
            var budgets = _budgetManager.GetBudgets(_userId);

            // Filter expenses by the selected year
            expenses = FilterExpensesByYear(expenses, year);

            var viewModel = new FinancialReportViewModel
            {
                Expenses = expenses,
                TotalIncome = expenses.Where(e => e.Type == TransactionType.Income).Sum(e => e.Amount),
                TotalExpenditure = expenses.Where(e => e.Type == TransactionType.Expense).Sum(e => e.Amount),
                CategorySummaries = CalculateCategorySummaries(expenses, categories, budgets),
                SelectedYear = year ?? DateTime.Now.Year
            };

            return View(viewModel);
        }

        private List<Expense> FilterExpensesByYear(List<Expense> expenses, int? year)
        {
            if (year.HasValue)
            {
                return expenses.Where(e => e.Date.Year == year).ToList();
            }
            else
            {
                return expenses;
            }
        }

        private int GetUserIdFromSession()
        {
            string customerId;
            // get user Id from the session
            var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "CustomerId");
            if (customerIdClaim != null)
            {
                customerId = customerIdClaim.Value;
                HttpContext.Session.SetInt32("CustomerId", Convert.ToInt32(customerIdClaim.Value));
                // Use customerId as needed
            }
            return Convert.ToInt32(HttpContext.Session.GetInt32("CustomerId"));
        }

        private List<FinancialReportViewModel.CategorySummary> CalculateCategorySummaries(List<Expense> expenses, List<Category> categories, List<Budget> budgets)
        {
            var categorySummaries = new List<FinancialReportViewModel.CategorySummary>();

            foreach (var category in categories)
            {
                var totalAmount = expenses.Where(e => e.CategoryId == category.CategoryId)
                                          .Sum(e => e.Type == TransactionType.Income ? e.Amount : -e.Amount);

                var budget = budgets.FirstOrDefault(b => b.CategoryId == category.CategoryId)?.Amount ?? 0;
                var isOverBudget = totalAmount > budget;

                categorySummaries.Add(new FinancialReportViewModel.CategorySummary
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    TotalAmount = totalAmount,
                    Budget = budget,
                    IsOverBudget = isOverBudget
                });
            }

            return categorySummaries;
        }
    }
}
