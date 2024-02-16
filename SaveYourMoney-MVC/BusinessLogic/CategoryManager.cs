using System;
using SaveYourMoney_MVC.Models;
using SaveYourMoney_MVC.Repositories;

namespace SaveYourMoney_MVC.BusinessLogic
{
	public class CategoryManager : ICategoryManager
	{
        private readonly AppDbContext _dbContext;

		public CategoryManager(AppDbContext dbContext)
		{
            _dbContext = dbContext;
		}

        public List<Category> GetCategories()
        {
            var categories = _dbContext.Categories.ToList();

            return categories;
        }
    }
}

