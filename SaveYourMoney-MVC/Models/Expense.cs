using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveYourMoney_MVC.Models
{
    public enum TransactionType
    {
        Income,
        Expense
    }

    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }

        public string ExpenseTitle { get; set; }

        public TransactionType Type { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("Category")]
        public string CategoryId { get; set; }

        public double Amount { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        // Property to store the file data
        public byte[] AttachmentData { get; set; }

        // Property to store the file name with extension
        public string AttachmentFileName { get; set; }
    }
}
