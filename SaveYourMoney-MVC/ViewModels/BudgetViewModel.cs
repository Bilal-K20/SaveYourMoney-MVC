using System;
using SaveYourMoney_MVC.Models;

namespace SaveYourMoney_MVC.ViewModels
{
	public class BudgetViewModel
	{
		public List<Budget> Budgets { get; set; }
		public List<Category> Categories { get; set; }
    }
}

