using System;
using System.Text.RegularExpressions;
using SaveYourMoney_MVC.Models;
using SaveYourMoney_MVC.Repositories;

namespace SaveYourMoney_MVC.BusinessLogic
{
    public class BudgetManager : IBudgetManager
    {
        private readonly AppDbContext _dbContext;

        public BudgetManager(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddBudget(int customerId, int categoryId , double amount, string? description, DateTime date)
        {

            // Validate inputs
            if (!IsValidCustomerId(customerId))
                throw new ArgumentException("Invalid customer ID");

            if (!IsValidCategoryId(categoryId))
                throw new ArgumentException("Invalid category ID");

            if (!IsValidAmount(amount))
                throw new ArgumentException("Invalid amount");

            if (!IsValidDescription(description))
                throw new ArgumentException("Invalid description");

            if (!IsValidDate(date))
                throw new ArgumentException("Invalid date");

            try
            {


                var newBudget = new Budget() {
                    CustomerId = customerId,
                    CategoryId = categoryId,
                    Amount = amount,
                    Date = date,
                    Description = description
                };

                _dbContext.Add(newBudget);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }


        public List<Budget> GetBudgets(int customerId)
        {
            List<Budget> budgets;
            try
            {
                
                 budgets = _dbContext.Budgets.ToList();

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                throw new Exception("Something went horribly wrong!");
            }

            return budgets;
        }

        public List<Budget> GetBudgetsForACategory(int customerId, int categoryId)
        {
            throw new NotImplementedException();
        }

        // Validate Customer ID (example regex pattern)
        private bool IsValidCustomerId(int customerId)
        {
            //string pattern = @"^\d{4}$"; // Example pattern: Customer ID should be a 4-digit number
            //return Regex.IsMatch(customerId.ToString(), pattern);

            // 9999 -> 10,000 users 
            return customerId > 0 && customerId < 99999;
        }

        // Validate Category ID (example regex pattern)
        private bool IsValidCategoryId(int categoryId)
        {
            //string pattern = @"^\d{2}$"; // Example pattern: Category ID should be a 2-digit number
            //return Regex.IsMatch(categoryId.ToString(), pattern);

            return categoryId >= 0 && categoryId <= 100;
        }

        private bool IsValidDate(DateTime date)
        {
            // Validate that the date is within the last 3 months and not more than 1 month in the future
            var currentDate = DateTime.Now;
            var threeMonthsAgo = currentDate.AddMonths(-3);
            var oneMonthAhead = currentDate.AddMonths(1);

            return date >= threeMonthsAgo && date <= oneMonthAhead;
        }
        // Validate Amount (example regex pattern)
        private bool IsValidAmount(double amount)
        {
            // Example pattern: Amount should be a positive number
            return amount > 0;
        }

        // Validate Description (example regex pattern)
        private bool IsValidDescription(string? description)
        {
            // Example pattern: Description can contain alphanumeric characters and spaces
            if (string.IsNullOrEmpty(description))
                return true; // Allow null or empty description
            string pattern = @"^[a-zA-Z0-9\s]+$";
            return Regex.IsMatch(description, pattern);
        }
    }

}

