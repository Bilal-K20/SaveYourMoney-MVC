using System;
namespace SaveYourMoney_MVC.Repositories
{
    using System.Collections.Generic;
    using System.Globalization;
    using Microsoft.EntityFrameworkCore;
    using SaveYourMoney_MVC.Models;

    public class AppDbContext : DbContext, IRepository
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        public void Add(Expense expense)
        {
            Expenses.Add(expense);
            SaveChanges();
        }

        public IEnumerable<Expense> GetExpensesByCustomerId(int customerId)
        {
            return Expenses.Where(e => e.CustomerId == customerId);
        }

        public Expense GetExpenseById(int customerId, int expenseId)
        {
            return Expenses.FirstOrDefault(e => e.CustomerId == customerId && e.ExpenseId == expenseId);
        }

        public bool DeleteExpense(int customerId, int expenseId)
        {
            var expense = GetExpenseById(customerId, expenseId);
            if (expense != null)
            {
                Expenses.Remove(expense);
                SaveChanges();
                return true;
            }
            return false;
        }

        public void UpdateExpense(int customerId, Expense expense)
        {
            var existingExpense = GetExpenseById(customerId, expense.ExpenseId);
            if (existingExpense != null)
            {
                existingExpense.ExpenseTitle = expense.ExpenseTitle;
                existingExpense.Type = expense.Type;
                existingExpense.Description = expense.Description;
                existingExpense.Date = expense.Date;
                existingExpense.CategoryId = expense.CategoryId;
                existingExpense.Amount = expense.Amount;
                existingExpense.AttachmentFileName = expense.AttachmentFileName;
                existingExpense.AttachmentData = expense.AttachmentData;

                SaveChanges();
            }
        }

        public IEnumerable<Expense> Search(string searchString)
        {
            return Expenses.Where(x => x.ExpenseTitle.Contains(searchString));
        }

        public IEnumerable<Expense> FilterExpenses(int userId, string date, string type, int? year, string month)
        {
            var expenses = Expenses.Where(e => e.CustomerId == userId).Include(e => e.Category).AsQueryable();

            if (!string.IsNullOrEmpty(date))
            {
                expenses = expenses.Where(e => e.Date == DateTime.Parse(date));
            }

            if (!string.IsNullOrEmpty(type))
            {
                var expenseType = Enum.Parse<TransactionType>(type);
                expenses = expenses.Where(e => e.Type == expenseType);
            }

            if (year.HasValue)
            {
                expenses = expenses.Where(e => e.Date.Year == year);
            }

            if (!string.IsNullOrEmpty(month))
            {
                var selectedMonth = DateTime.ParseExact(month, "MMMM", CultureInfo.InvariantCulture);
                expenses = expenses.Where(e => e.Date.Month == selectedMonth.Month);
            }

            return expenses.ToList();
        }

        // Other methods...

        public List<string> GetMonthsList()
        {
            return DateTimeFormatInfo
                .InvariantInfo
                .MonthNames
                .Where(m => !string.IsNullOrEmpty(m))
                .OrderBy(m => m)
                .ToList();
        }
    }

}

