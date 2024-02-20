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

        public void AddCategory(string categoryName)
        {
            // Validation logic can be added here if needed

            // Create new category entity
            //var category = new Category {CusomterId=  CategoryName = categoryName };

            // Add category to the database
            //_categoryRepository.AddCategory(category);
        }

        public void AddCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}

