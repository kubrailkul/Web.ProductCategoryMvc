using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ProductCategoryMvc.Models;

namespace Web.ProductCategoryMvc.Controllers
{
    public class HomeController : Controller
    {
        ProjeDBEntities db = new ProjeDBEntities();

        // GET: Home
        public ActionResult Index()
        {
            var categoryList = db.tblCategories.ToList();
            var productList = db.tblProducts.ToList();
            var categoryListViewModel = new ProductCategoryListViewModel
            {
                Categories = categoryList,
                Products=productList
            };

            return View(categoryListViewModel);
        }
    }
}