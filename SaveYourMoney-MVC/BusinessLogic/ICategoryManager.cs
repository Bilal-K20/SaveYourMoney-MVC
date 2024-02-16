using System;
using SaveYourMoney_MVC.Models;

namespace SaveYourMoney_MVC.BusinessLogic
{
	public interface ICategoryManager
	{
		List<Category> GetCategories();

	}
}

