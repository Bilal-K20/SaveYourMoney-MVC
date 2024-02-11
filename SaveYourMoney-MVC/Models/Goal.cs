using System;
namespace SaveYourMoney_MVC.Models
{
	public class Goal
	{
		public int Id { get; set; }
		public int CustomerId { get; set; }
        public int TargetMonthlySaving { get; set; }
		public int TargetAnnualSaving { get; set; }
		public string Description { get; set; }
    }
}

