using System;
using System.ComponentModel.DataAnnotations;

namespace SaveYourMoney_MVC.Models
{
	public class Customer
	{
		[Key]
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
    }
}

