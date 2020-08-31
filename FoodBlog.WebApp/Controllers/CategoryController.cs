using FoodBlog.BusinessLayer;
using FoodBlog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FoodBlog.WebApp.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category with TempData
        //public ActionResult Get(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    CategoryManager cm = new CategoryManager();
        //    Category category = cm.GetCategoryId(id.Value);

        //    if (category == null)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        
        //    TempData["categoryGet"] = category.Foods;
        //    return RedirectToAction("Index", "Home");
        //}
    }
}