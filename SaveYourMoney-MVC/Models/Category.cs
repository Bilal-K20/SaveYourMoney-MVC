using System;
namespace SaveYourMoney_MVC.Models
{
	public class Category
	{
		public int Id { get; set; }
		public int CusomterId { get; set; }
        public int BudgetId { get; set; }
        public string CategoryName { get; set; }
	}
}

