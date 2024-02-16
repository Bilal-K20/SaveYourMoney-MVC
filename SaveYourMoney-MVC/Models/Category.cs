using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveYourMoney_MVC.Models
{
	public class Category
	{
		public int CategoryId { get; set; }

		[ForeignKey("Customer")]
		public int CusomterId { get; set; }

		[ForeignKey("Budget")]
        public int? BudgetId { get; set; }

        public string CategoryName { get; set; }
	}
}

