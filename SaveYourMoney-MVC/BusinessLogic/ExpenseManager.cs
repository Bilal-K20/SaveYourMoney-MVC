using System;
using SaveYourMoney_MVC.Models;
using SaveYourMoney_MVC.Repositories;

namespace SaveYourMoney_MVC.BusinessLogic
{
	public class ExpenseManager : IExpenseManager
	{
        private readonly AppDbContext _dbContext;

        public ExpenseManager(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

        public bool DeleteExpense(int customerId, int expenseId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Expense> GetAllExpenses(int customerId)
        {
            List<Expense> expenses = null;
            try
            {
                expenses = _dbContext.Expenses.Where(e => e.CustomerId == customerId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not find any expenses for the user Id provided");
            }
            return expenses;
        }

        public Expense GetExpenseById(int customerId, int expenseId)
        {
            Expense expense = null;

            try
            {
                expense = _dbContext.Expenses.FirstOrDefault(e => e.CustomerId == customerId && e.ExpenseId == expenseId);

                if (expense == null)
                {
                    throw new Exception("Could not find expense for the user!, please ensure you have created your expense");
                }
                
            }
            catch (Exception ex)
            {
                var e = ex.Message;
            }
            return expense;
        }

        public List<Expense> GetExpenseBySearch(int customerId, string search)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Expense> Search (string searchString)
		{
			List<Expense>? searchExpenses;

            try
			{
				searchExpenses = _dbContext.Expenses.Where(x => x.ExpenseTitle.Contains(searchString)).ToList();
				return searchExpenses;
			}
			catch (Exception ex)
			{
				var e = ex.Message;
				throw; 
			}
			return searchExpenses;
		}

        public void UpdateExpense(int customerId, Expense expense)
        {
            throw new NotImplementedException();
        }
    }
}

