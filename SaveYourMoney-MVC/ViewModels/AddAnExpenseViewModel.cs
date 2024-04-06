using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using SaveYourMoney_MVC.Models;

namespace SaveYourMoney_MVC.ViewModels
{
    public enum ExpenseType
    {
        //income = 0, expense = 1
        Income,
        Expense
    }
    public class AddAnExpenseViewModel
	{
		public string ExpenseTitle { get; set; }
		public ExpenseType ExpenseType { get; set; }
		public double Amount { get; set; }
		public List<SelectListItem> Categories { get; set; }
		public int SelectedCategoryId { get; set; }
        public string ExpenseDescription { get; set; }
		public DateTime Date { get; set; }
        public string AttachmentName { get; set; }
        //public byte[] AttachmentData { get; set; }
        public IFormFile AttachmentData { get; set; }


    }
}

