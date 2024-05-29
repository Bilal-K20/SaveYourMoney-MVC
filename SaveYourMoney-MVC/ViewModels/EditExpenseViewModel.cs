using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SaveYourMoney_MVC.Models;

namespace SaveYourMoney_MVC.ViewModels
{



    public class EditExpenseViewModel
    {
        [Required(ErrorMessage = "Expense ID is required.")]
        public int ExpenseId { get; set; }

        [Required(ErrorMessage = "Expense title is required.")]
        public string ExpenseTitle { get; set; }

        [Required(ErrorMessage = "Expense type is required.")]
        public ExpenseType OldExpenseType { get; set; }

        public ExpenseType NewExpenseType { get; set; }


        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Amount must have up to 2 decimal places.")]
        public double Amount { get; set; }

        public int OldCategoryId { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int SelectedCategoryId { get; set; } // Added [Required] attribute

        public List<SelectListItem> Categories { get; set; }

        public string? ExpenseDescription { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public string? AttachmentName { get; set; }

        public IFormFile? AttachmentData { get; set; }
    }
}
