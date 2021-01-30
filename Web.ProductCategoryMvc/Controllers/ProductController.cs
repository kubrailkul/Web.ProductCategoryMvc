using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ProductCategoryMvc.Models;

namespace Web.ProductCategoryMvc.Controllers
{
    public class ProductController : Controller
    {
        ProjeDBEntities db = new ProjeDBEntities();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var productList = db.tblProducts.ToList();
            var categoryList = db.tblCategories.ToList();
            var productListViewModel = new ProductCategoryListViewModel

            {
                Products = productList,
                Categories=categoryList

            };

            return View(productListViewModel);
        }

        public ActionResult Edit(int? id)
        {
            List<SelectListItem> Categories = new List<SelectListItem>();

            foreach (tblCategory item in db.tblCategories)
            {
                Categories.Add(new SelectListItem()
                {
                    Text = item.CategoryName,
                    Value = item.Id.ToString()
                });
            }

            ViewBag.Categories = Categories;
       

            if (id == null)
            {
                return View();
            }
            else
            {            
                var model = db.tblProducts.FirstOrDefault(x => x.Id == id);

                return View(model);
            }

        }

        [HttpPost]
        public ActionResult Edit(tblProduct model)
        {
           

            if (model.Id == 0)
            {
                db.tblProducts.Add(model);
            }
            else
            {
                var data = db.tblProducts.FirstOrDefault(x => x.Id == model.Id);
                data.ProductName = model.ProductName;
                data.CategoryId = model.CategoryId;
            }

            db.SaveChanges();

            return RedirectToAction("List", "Product");
        }

        public ActionResult Delete(int? id)
        {
            var data = db.tblProducts.FirstOrDefault(x => x.Id == id);

            if (data != null)
            {
                db.tblProducts.Remove(data);
                db.SaveChanges();
            }
            return RedirectToAction("List", "Product");
        }


    }
}