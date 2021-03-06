﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using Web.ProductCategoryMvc.Models;

namespace Web.ProductCategoryMvc.Controllers
{
    public class CategoryController : Controller
    {

        ProjeDBEntities db = new ProjeDBEntities();

        public ActionResult Index()
        {
            return View();
        }

        // GET: Categories
        public ActionResult List()
        {
            var categoryList = db.tblCategories.ToList();
            var categoryListViewModel = new ProductCategoryListViewModel
            {
                Categories = categoryList
            };

            return View(categoryListViewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View();
            }
            else
            {
                var model = db.tblCategories.FirstOrDefault(x => x.Id == id);
                return View(model);
            }

        }

        //Add or update

        [HttpPost]
        public ActionResult Edit(tblCategory model)
        {
            if (model.Id == 0)
            {
                db.tblCategories.Add(model);
            }
            else
            {
                var data = db.tblCategories.FirstOrDefault(x => x.Id == model.Id);
                data.CategoryName = model.CategoryName;
            }

            db.SaveChanges();                   
            return RedirectToAction("List", "Category");
        }


        [HttpPost]
        public JsonResult Delete(int? id)
        {
            var data = db.tblCategories.FirstOrDefault(x => x.Id == id);
            var foreignkeyexception_kontrol = db.tblProducts.FirstOrDefault(x => x.CategoryId == id);
            try
            {

                if (foreignkeyexception_kontrol == null)
                {
                    db.tblCategories.Remove(data);
                    db.SaveChanges();
                    string sayi = db.tblCategories.ToList().Count.ToString();
                    return Json(sayi, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json("-1", JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return Json("-2", JsonRequestBehavior.AllowGet);

            }

        }


    }
}