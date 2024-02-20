using System;
using Microsoft.AspNetCore.Identity;

namespace SaveYourMoney_MVC.Entities
{
	public class ApplicationUser : IdentityUser
	{
		public int CustomerId { get; set; }
	}
}

