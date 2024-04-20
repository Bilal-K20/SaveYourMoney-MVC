using System;
using SaveYourMoney_MVC.Models;
using SaveYourMoney_MVC.ViewModels;

namespace SaveYourMoney_MVC.BusinessLogic
{
	public interface ICategoryManager
	{
        //List<Category> GetCategories();
        // this makes more sense because it will only get the categories for that one particular customer.
        List<Category> GetCategories(int customerId);
        void AddCategory(int? customerId, string categoryName);
        Category GetCategoryByCategoryId(int? customerId,int categoryId);
        void UpdateCategory(int? customerId,int categoryId ,string categoryName);
        void DeleteCategory(int? customerId, int categoryId);
        //decimal? GetBudgetAmountForCategory(int customerId,int categoryId);


    }
}

