using System;
namespace SaveYourMoney_MVC.Models
{
	public class Expense
	{
		public int ExpenseId { get; set; }
		public string Type { get; set; }
		public string CategoryId { get; set; }
		public double Amount { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }
    }
}

