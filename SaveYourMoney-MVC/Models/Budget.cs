namespace SaveYourMoney_MVC.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public int Category { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}