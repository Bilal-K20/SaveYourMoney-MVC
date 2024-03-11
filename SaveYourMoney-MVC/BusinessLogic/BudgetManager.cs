using System;
using SaveYourMoney_MVC.Models;
using SaveYourMoney_MVC.Repositories;

namespace SaveYourMoney_MVC.BusinessLogic
{
    public class BudgetManager : IBudgetManager
    {
        private readonly AppDbContext _dbContext;

        public BudgetManager(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public void AddBudget(int customerId, int categoryId , double amount, string? description)
        {
            try
            {
                var newBudget = new Budget() {
                    CustomerId = customerId,
                    CategoryId = categoryId,
                    Amount = amount,
                    Description = description
                };

                _dbContext.Add(newBudget);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }


        public List<Budget> GetBudgets(int customerId)
        {
            List<Budget> budgets;
            try
            {
                
                 budgets = _dbContext.Budgets.ToList();

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                throw new Exception("Something went horribly wrong!");
            }

            return budgets;
        }

        public List<Budget> GetBudgetsForACategory(int customerId, int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}

