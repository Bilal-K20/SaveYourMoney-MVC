using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using SaveYourMoney_MVC.Models;
using SaveYourMoney_MVC.Repositories;
using SaveYourMoney_MVC.ViewModels;

namespace SaveYourMoney_MVC.BusinessLogic
{
	public class ExpenseManager : IExpenseManager
	{
        private readonly AppDbContext _dbContext;

        public ExpenseManager(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

        public void AddExpense(int userId, AddAnExpenseViewModel model)
        {
            try
            {
                    if (IsNeccessaryDataPresent(model))
                    {
                        // maybe check for additional data if it is available then validate that too
                        Expense newExpense = new Expense();
                        newExpense.CustomerId = userId;
                        newExpense.ExpenseTitle = model.ExpenseTitle;
                        newExpense.Type = (TransactionType)model.ExpenseType;
                        newExpense.Description = model.ExpenseDescription;

                        newExpense.Date = model.Date;

                    // I am not sure why is set this as 
                        newExpense.CategoryId = model.SelectedCategoryId;
                        newExpense.Amount = model.Amount;


                    // Handle attachment - code according to MS documentation you have to use Memory stream.
                    if (model.AttachmentData != null && model.AttachmentData.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            model.AttachmentData.CopyTo(ms);
                            newExpense.AttachmentFileName = model.AttachmentName;
                            newExpense.AttachmentData = ms.ToArray();
                        }
                    }

                    _dbContext.Add(newExpense);
                    _dbContext.SaveChanges();
                    }

            }
            catch (Exception ex)
            {
                throw new Exception($"Oops something went horribly wrong! Here is the error: {ex.Message}");
            }
        }

        public bool DeleteExpense(int customerId, int expenseId)
        {
            throw new NotImplementedException();
        }

        public List<Expense> GetAllExpenses(int customerId)
        {
            List<Expense> expenses = new List<Expense>();

            try
            {
                if (customerId >= 0)
                {

                    expenses = _dbContext.Expenses.Where(e => e.CustomerId == customerId).ToList();


                }
                else
                {
                    //chances are the cookie/session (not sure what is the right terminology) is not storing the correct details
                    // or it is not being passed to this method for whatever reason.
                    throw new ArgumentException("The customer Id cannot be null or any number less than 0 !");
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                throw new Exception($"An fatal error occured when trying to retrieve expenses", ex);
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


        /**
         * Might be better to include a structure like this
         * IsNeccessaryDataPresent i think shoukd only valide if the data is present
         * IsDataInCorrect Format could be a another method
         * the thing is if there is an Attachment title then it must look for a valid datZ
         * 
         * this method is doing two things, checking the format of thr input and checking if mandatory fields are present. 
         * 
         */

        public bool IsNeccessaryDataPresent(AddAnExpenseViewModel model)
        {
            bool isAllDataValidated = false;
            try
                {
                if (!string.IsNullOrWhiteSpace(model.ExpenseTitle) &&
                    model.ExpenseType != null &&
                    !double.IsNegative(model.Amount) &&
                        model.Amount > 0 &&
                        IsDateValid(model.Date))
                    {
                    // Validate Expense Title
                    //expense title where the title can contain 1 to 25 chars and cannot contain special
                    //characters ^[a-zA-Z0-9\s]{1,25}$

                    if (!Regex.IsMatch(model.ExpenseTitle, @"^[a-zA-Z0-9\s]+$"))
                    {
                            // Title can only contain letters and numbers
                            return false;
                    }

                        // Validate Amount
                        var amountRegex = new Regex(@"^\d+(\.\d{1,2})?$");
                        if (!amountRegex.IsMatch(model.Amount.ToString()))
                        {
                            // Amount must be a valid positive number with up to two decimal places
                            return false;
                        }

                        // Validate Attachment Name if provided
                        if (!string.IsNullOrWhiteSpace(model.AttachmentName))
                        {
                            if (!Regex.IsMatch(model.AttachmentName, @"^[a-zA-Z0-9\s]+$"))
                            {
                                // Attachment name can only contain letters and numbers
                                return false;
                            }
                        }

                        // Validate Date
                        var currentDate = DateTime.Today;
                        var minDate = currentDate.AddYears(-2); // One year ago
                        var maxDate = currentDate.AddYears(3); // Two years in the future

                        if (model.Date < minDate || model.Date > maxDate)
                        {
                            // Date must be within the specified range
                            return false;
                        }

                        // All data is validated
                        isAllDataValidated = true;
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                    var errorMessage = ex.Message;
                }

            return isAllDataValidated;
        }

        public ExpenseViewModel FilterExpenses(string date, string type)
        {
            var newViewModel = new ExpenseViewModel();
            try
            {
                var expenses = _dbContext.Expenses.Include(e => e.Category).AsQueryable();

                if (!string.IsNullOrEmpty(date))
                {
                    expenses = expenses.Where(e => e.Date == DateTime.Parse(date));
                }

                if (!string.IsNullOrEmpty(type))
                {
                    // Convert enum string to enum value
                    var expenseType = Enum.Parse<TransactionType>(type);
                    expenses = expenses.Where(e => e.Type == expenseType);
                }

                newViewModel.Expenses = expenses.ToList();

                newViewModel.Categories = _dbContext.Categories.ToList();
            }
            catch (Exception ex)
            {
                var e = ex.Message;
            }

            return newViewModel;
   
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

        /** IsDateValid
         * today: gets todays date 
         * minDate: oldest date that is accepted
         * maxDate: max date that can be set. 
         *
         * The idea is that someone could be saving up for a house or someone could use it to predict financial status in future
         */

        private bool IsDateValid (DateTime date)
        {
            var today = DateTime.Today;
            var minDate = today.AddYears(-1);
            var maxDate = today.AddYears(10);

            if (date >= minDate && date <= maxDate)
            {
                return true;
            }
            else{
                return false;
            }
        }
        
    }
}

