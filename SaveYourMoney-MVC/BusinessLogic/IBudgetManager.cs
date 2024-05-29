using System;
using SaveYourMoney_MVC.Models;

namespace SaveYourMoney_MVC.BusinessLogic
{
	public interface IBudgetManager
	{
		List<Budget> GetBudgets(int customerId);
		List<Budget> GetBudgetsForACategory(int customerId, int categoryId);
		void AddBudget(int customerId, int categoryId ,double amount, string? description, DateTime date);
		Budget GetBudgetForACategory(int? customerId, int budgetId);
        void UpdateBudget(int? customerId, int budgetId, int newCategoryId, double newAmount, string? newDescription, DateTime newDate);
        void DeleteBudget(int? customerId, int budgetId);
    }
}

