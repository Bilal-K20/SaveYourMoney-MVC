using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveYourMoney_MVC.Models
{
	public class Category
	{
		public int CategoryId { get; set; }

		[ForeignKey("Customer")]
		public int CustomerId { get; set; }
        public Customer Customer { get; set; } // Navigation property
        public string CategoryName { get; set; }
	}
}

