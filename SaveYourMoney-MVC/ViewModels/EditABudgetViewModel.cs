using System;
using SaveYourMoney_MVC.Models;

namespace SaveYourMoney_MVC.ViewModels
{
	public class EditABudgetViewModel
	{
       
            public int BudgetId { get; set; }
            public int OldCategoryId { get; set; }
            public int NewCategoryId { get; set; }
            public double Amount { get; set; }
            public List<Category> Categories { get; set; }
            public string CategoryName { get; set; }
            public string Description { get; set; }
            public DateTime Date { get; set; }
            // Add any additional properties needed for editing
        
    }
}

