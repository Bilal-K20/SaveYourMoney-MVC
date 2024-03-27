using System;
using SaveYourMoney_MVC.Models;

namespace SaveYourMoney_MVC.ViewModels
{
	public class AddANewBudgetViewModel
	{
		public double Amount { get; set; }
		public List<Category> Categories { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }
		public int SelectedCategoryId { get; set; }

	}
}

