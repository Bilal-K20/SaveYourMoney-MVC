using System;
using SaveYourMoney_MVC.Models;

namespace SaveYourMoney_MVC.ViewModels
{
	public class ExpenseViewModel
	{
		public string ExpenseTitle { get; set; }

		public string Description { get; set; }

		//transaction type 1. income 2. expense

		public double Amount { get; set; }

		public DateTime Date { get; set; }

		// list of categories not sure if i should use ienumerable or list 
		public IEnumerable<Category> Categories { get; set; }
		public IEnumerable<Expense> Expenses { get; set; }

        //property to attach an attachment such as an image/receipt/pdf  

    }
}

