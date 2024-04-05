using System;
using SaveYourMoney_MVC.Models;

namespace SaveYourMoney_MVC.ViewModels
{
	public class TransactionViewModel
	{
		List<Expense> Expenses;
		List<Category> Categories;
		List<Budget> Budgets;

		public double TotalExpenditure { get; set; }
	}
}

