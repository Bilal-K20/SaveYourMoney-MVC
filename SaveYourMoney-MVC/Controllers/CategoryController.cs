using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaveYourMoney_MVC.BusinessLogic;
using SaveYourMoney_MVC.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SaveYourMoney_MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryManager CategoryManager;

        public CategoryController(ICategoryManager categoryManager)
        {
            CategoryManager = categoryManager;
        }

        // GET: /<controller>/
        [Authorize]
        [HttpGet]
        public IActionResult Category()
        {
            var categoryViewModel = new CategoryViewModel();

            var listOfCategories = CategoryManager.GetCategories();

            categoryViewModel.Categories = listOfCategories;

            return View(categoryViewModel);
        }
    }
}

