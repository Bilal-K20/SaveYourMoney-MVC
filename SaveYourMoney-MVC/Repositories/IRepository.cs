using System;
using Microsoft.EntityFrameworkCore;
using SaveYourMoney_MVC.Models;

namespace SaveYourMoney_MVC.Repositories
{
	public interface IRepository 
	{
        void Add(Expense expense);
        IEnumerable<Expense> GetExpensesByCustomerId(int customerId);
        Expense GetExpenseById(int customerId, int expenseId);
        bool DeleteExpense(int customerId, int expenseId);
        void UpdateExpense(int customerId, Expense expense);
        IEnumerable<Expense> Search(string searchString);
        IEnumerable<Expense> FilterExpenses(int userId, string date, string type, int? year, string month);
        List<string> GetMonthsList();
    }
}

