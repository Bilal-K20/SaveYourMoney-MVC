using System;
using SaveYourMoney_MVC.Models;

namespace SaveYourMoney_MVC.BusinessLogic
{
	public interface IBudgetManager
	{
		List<Budget> GetBudgets(int customerId);
		List<Budget> GetBudgetsForACategory(int customerId, int categoryId);
		void AddBudget(int customerId, int categoryId ,double amount, string? description);
    }
}

