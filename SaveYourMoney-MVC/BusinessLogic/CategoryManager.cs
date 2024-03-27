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
            // Validation logic can be added here if needed

            //create a new category object

           

            // It MUST have to populate it with a customer id and categoryname
            //At this stage budget id should be null because this has to be set in the add a budget page


            // Create new category entity

            // add user properties to a category



            // Add category to the database
            //_categoryRepository.AddCategory(category);
        }

        public void AddCategory(Category category)
        {
            throw new NotImplementedException();
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
    }
}

