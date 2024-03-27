using System;
namespace SaveYourMoney_MVC.ViewModels
{
	public class EditABudgetViewModel
	{
       
            public int BudgetId { get; set; }
            public string Amount { get; set; }
            public string CategoryName { get; set; }
            public string Description { get; set; }
            public DateTime Date { get; set; }
            // Add any additional properties needed for editing
        
    }
}

