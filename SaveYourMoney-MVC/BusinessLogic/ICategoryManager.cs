using System;
using SaveYourMoney_MVC.Models;

namespace SaveYourMoney_MVC.BusinessLogic
{
	public interface ICategoryManager
	{
        List<Category> GetCategories();
        // this makes more sense because it will only get the categories for that one particular customer.
        List<Category> GetCategories(int customerId);
        void AddCategory(int? customerId, string categoryName);
        //decimal? GetBudgetAmountForCategory(int customerId,int categoryId);


    }
}

