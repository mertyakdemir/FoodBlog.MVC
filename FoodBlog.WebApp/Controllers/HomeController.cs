using FoodBlog.BusinessLayer;
using FoodBlog.Entities;
using FoodBlog.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FoodBlog.WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            // Category List with TempData
            //if( TempData["categoryGet"] != null)
            //{
            //    return View(TempData["categoryGet"] as List<Food>);
            //}

            FoodManager food_m = new FoodManager();

            return View(food_m.GetFoods().OrderByDescending(x => x.Created).ToList());
        }

        public ActionResult GetCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoryManager cm = new CategoryManager();
            Category category = cm.GetCategoryId(id.Value);

            if (category == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("Index", category.Foods.OrderByDescending(x => x.Created).ToList());
        }

        public ActionResult PopularArticles()
        {
            FoodManager fm = new FoodManager();
            return View("Index", fm.GetFoods().OrderByDescending(x => x.LikeCount).ToList());
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                BlogUserManager _bloguser = new BlogUserManager();
                BusinessResult<BlogUsers> _bloguserRes = _bloguser.LoginUser(model);

                if(_bloguserRes.Errors.Count > 0)
                {
                    _bloguserRes.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }

                Session["login"] = _bloguserRes.Result;
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                BlogUserManager _bloguser = new BlogUserManager();
                BusinessResult<BlogUsers> _bloguserRes = _bloguser.RegisterUser(model);

                if(_bloguserRes.Errors.Count > 0)
                {
                    _bloguserRes.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }

                return RedirectToAction("RegisterSuccessful");
            }
            return View(model);
        }

        public ActionResult RegisterSuccessful()
        {
            return View();
        }

        public ActionResult ActivateUser(Guid id)
        {
            BlogUserManager _blogUser = new BlogUserManager();
            BusinessResult<BlogUsers> _result = _blogUser.ActivateUser(id);
            if(_result.Errors.Count > 0)
            {
                TempData["errors"] = _result.Errors;
                return RedirectToAction("ActivateError");
            }

            return RedirectToAction("ActivateUserOK");
        }

        public ActionResult ActivateError()
        {
           
            if (TempData["errors"] != null)
            {
                ModelState.AddModelError("", "Invalid activate");
            }
            return View();
        }

        public ActionResult ActivateUserOK()
        {
            return View();
        }

        public ActionResult ShowProfile()
        {
            BlogUsers user = Session["login"] as BlogUsers;

            BlogUserManager _blogUserManager = new BlogUserManager();
            BusinessResult<BlogUsers> _result = _blogUserManager.GetUser(user.Id);

            if(_result.Errors.Count > 0)
            {

            }

            return View(_result.Result);
        }

        public ActionResult EditProfile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(BlogUsers user)
        {
            return View();
        }

        public ActionResult RemoveProfile()
        {
            return View();
        }

    }
}