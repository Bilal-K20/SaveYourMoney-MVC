using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveYourMoney_MVC.Models
{
    public class Budget
    {
        [Key]
        public int BudgetId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } // Navigation property for Customer

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; } // Navigation property for Category


        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}