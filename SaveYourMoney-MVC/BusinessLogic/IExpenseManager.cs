using System;
using SaveYourMoney_MVC.Models;

namespace SaveYourMoney_MVC.BusinessLogic
{
	public interface IExpenseManager
	{
		IEnumerable<Expense> GetAllExpenses(int customerId);

		Expense GetExpenseById(int customerId, int expenseId);

		List<Expense> GetExpenseBySearch(int customerId,string search);

		void UpdateExpense(int customerId, Expense expense);

		bool DeleteExpense(int customerId, int expenseId);
	}
}

