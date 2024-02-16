using System;
using Microsoft.AspNetCore.Authorization;
using SaveYourMoney_MVC.Models;

namespace SaveYourMoney_MVC.ViewModels
{
	[Authorize]
	public class CategoryViewModel
	{
		public List<Category> Categories { get; set; }


	}
}

