using System;
namespace SaveYourMoney_MVC.ViewModels
{
	public class AddCategoryViewModel
	{
        public string UserId { get; set; }
        public string CategoryName { get; set; }
        /*
         * The reason I havent added a budget here is because a user needs to create a category first
         * Maybe once it is created then i should redirect to categories and have an option to view a button 
         * that allows user to set a budget for a category
         */
    }
}

