using System;
using SaveYourMoney_MVC.Models;
using SaveYourMoney_MVC.Repositories;

namespace SaveYourMoney_MVC.BusinessLogic
{
	public class CategoryManager : ICategoryManager
	{
        private readonly AppDbContext _dbContext;
        

        // when calling this manager is it worth including cusomterId in the constructor?

		public CategoryManager(AppDbContext dbContext)
		{
            _dbContext = dbContext;
		}

        public List<Category> GetCategories()
        {
            var categories = _dbContext.Categories.ToList();

            return categories;
        }

        public void AddCategory(int? customerId, string categoryName)
        {
            try
            {

                if (customerId != null || customerId != -1 && !string.IsNullOrWhiteSpace(categoryName))
                {

                    // validate category name using regex

                    var newCategory = new Category();
                    newCategory.CustomerId = (int)customerId;
                    newCategory.CategoryName = categoryName;

                    _dbContext.Categories.Add(newCategory);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
               var error = ex.Message;
            }
 
        }


        public List<Category> GetCategories(int customerId)
        {
            List<Category>? categories = new List<Category>();
            try
            {
                categories = _dbContext.Categories.Where(x => x.CustomerId == customerId).ToList();
            }
            catch (Exception ex)
            {
                var e = ex.Message;
            }
            return categories;

        }

        public Category GetCategoryByCategoryId(int? customerId , int categoryId)
        {
            var category = new Category();

            try
            {
                if (customerId.HasValue && (categoryId != null || categoryId != 0))
                {
                    category = _dbContext.Categories.FirstOrDefault(c => c.CategoryId == categoryId && c.CustomerId == customerId);
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception ex)
            {
                var e = ex.Message;
                throw;
            }
            return category;
        }

        public void UpdateCategory(int? customerId, int categoryId ,string categoryName)
        {
            try
            {
                var category = _dbContext.Categories.FirstOrDefault(c => c.CustomerId == customerId && c.CategoryId == categoryId);
                if(category != null)
                {
                    category.CategoryName = categoryName;
                    _dbContext.SaveChanges();
                }
                else
                {
                    var e = new ArgumentNullException();
                    throw e;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteCategory(int? customerId, int categoryId)
        {
            try
            {
                if(customerId != null || customerId != 0)
                {
                    var category =_dbContext.Categories.FirstOrDefault(c => c.CustomerId == customerId && c.CategoryId == categoryId);
                    _dbContext.Categories.Remove(category);
                    _dbContext.SaveChanges();
                }
                else
                {
                    //doesn't need a variable but i have done it just to understand how it works
                    var e = new ArgumentNullException();
                    throw e;
                }


            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

