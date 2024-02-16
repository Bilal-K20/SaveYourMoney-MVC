using System;
using Microsoft.AspNetCore.Authorization;
using SaveYourMoney_MVC.Models;

namespace SaveYourMoney_MVC.ViewModels
{
	[Authorize]
	public class DashboardViewModel
	{
		public string  FirstName{ get; set; }
		public string  LastName{ get; set; }
		public List<Expense>  Expenses{ get; set; }
		public List<Budget>  Budgets{ get; set; }
		public List<Goal>  Goals{ get; set; }
    }
}

