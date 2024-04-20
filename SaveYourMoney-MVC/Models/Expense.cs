using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveYourMoney_MVC.Models
{
    public enum TransactionType
    {
        //income = 0, expense = 1
        Income,
        Expense
    }

    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }

        //mandatory
        [Required]
        public string ExpenseTitle { get; set; }

        //mandatory
        [Required]
        public TransactionType Type { get; set; }

        //mandatory
        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        //prefferable
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }

        //mandatory
        [Required]
        public double Amount { get; set; }

        public string? Description { get; set; }

        //mandatory
        [Required]
        public DateTime Date { get; set; }

        // Property to store the file data
        public byte[]? AttachmentData { get; set; }

        // Property to store the file name with extension
        public string? AttachmentFileName { get; set; }
    }
}
