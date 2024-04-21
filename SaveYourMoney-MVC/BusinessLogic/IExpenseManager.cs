using System;
using SaveYourMoney_MVC.Models;
using SaveYourMoney_MVC.ViewModels;

namespace SaveYourMoney_MVC.BusinessLogic
{
	public interface IExpenseManager
	{
		List<Expense> GetAllExpenses(int customerId);

		Expense GetExpenseById(int customerId, int expenseId);

		List<Expense> GetExpenseBySearch(int customerId,string search);
		ExpenseViewModel FilterExpenses(string date, string type, int? year, string? month);
        void UpdateExpense(int customerId, Expense expense);

		bool DeleteExpense(int customerId, int expenseId);
        bool IsNeccessaryDataPresent(AddAnExpenseViewModel model);

		void AddExpense(int userId, AddAnExpenseViewModel model);
		List<string> GetMonthsList();
    }
}

