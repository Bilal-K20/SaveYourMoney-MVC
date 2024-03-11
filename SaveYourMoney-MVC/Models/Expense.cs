using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveYourMoney_MVC.Models
{
	public class Expense
	{
		[Key]
		public int ExpenseId { get; set; }
		public string Type { get; set; }

        //[ForeignKey]
		public string CategoryId { get; set; }
		public double Amount { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }
    }
}

