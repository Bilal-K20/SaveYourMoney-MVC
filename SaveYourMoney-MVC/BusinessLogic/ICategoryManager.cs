using System;
using SaveYourMoney_MVC.Models;

namespace SaveYourMoney_MVC.BusinessLogic
{
	public interface ICategoryManager
	{
        List<Category> GetCategories();
        void AddCategory(int? customerId, string categoryName);
        //decimal? GetBudgetAmountForCategory(int customerId,int categoryId);


    }
}

