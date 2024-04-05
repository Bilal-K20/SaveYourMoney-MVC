using System;
namespace SaveYourMoney_MVC.ViewModels
{
	public class EditCategoryViewModel
	{
		public int CategoryId { get; set; }
		public string NewCategoryName { get; set; }
		public string OldCategoryName { get; set; }
	}
}

