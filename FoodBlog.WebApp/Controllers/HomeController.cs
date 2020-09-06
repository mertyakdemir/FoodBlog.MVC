using FoodBlog.BusinessLayer;
using FoodBlog.Entities;
using FoodBlog.Entities.ViewModels;
using FoodBlog.WebApp.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FoodBlog.WebApp.Controllers
{
    [ErrorPage]
    public class HomeController : Controller
    {
        private FoodManager foodManager = new FoodManager();
        private CategoryManager categoryManager = new CategoryManager();
        private BlogUserManager blogUserManager = new BlogUserManager();
        // GET: Home
        public ActionResult Index()
        {   
            // Category List with TempData
            //if( TempData["categoryGet"] != null)
            //{
            //    return View(TempData["categoryGet"] as List<Food>);
            //}

            return View(foodManager.ListQueryable().Where(x => x.IsDraft == false).OrderByDescending(x => x.Created).ToList());
        }

        public ActionResult FoodPage(int? id)
        {
            if( id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = foodManager.Find(x => x.Id == id);

            if(food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        public ActionResult GetCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = categoryManager.Find(x => x.Id == id.Value);

            if (category == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("Index", category.Foods.Where(x => x.IsDraft == false).OrderByDescending(x => x.Created).ToList());
        }

        public ActionResult PopularArticles()
        {
            return View("Index", foodManager.ListQueryable().OrderByDescending(x => x.LikeCount).ToList());
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
                BusinessResult<BlogUsers> _bloguserRes = blogUserManager.LoginUser(model);

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
                BusinessResult<BlogUsers> _bloguserRes = blogUserManager.RegisterUser(model);

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
            BusinessResult<BlogUsers> _result = blogUserManager.ActivateUser(id);
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

        [Authority]
        public ActionResult ShowProfile()
        {
            BlogUsers user = Session["login"] as BlogUsers;

            BusinessResult<BlogUsers> _result = blogUserManager.GetUser(user.Id);

            if(_result.Errors.Count > 0)
            {
                ModelState.AddModelError("", "Error");
            }

            return View(_result.Result);
        }

        [Authority]
        public ActionResult EditProfile()
        {
            BlogUsers user = Session["login"] as BlogUsers;

            BusinessResult<BlogUsers> _result = blogUserManager.GetUser(user.Id);

            if (_result.Errors.Count > 0)
            {
                ModelState.AddModelError("", "Error");
            }

            return View(_result.Result);
        }

        [Authority]
        [HttpPost]
        public ActionResult EditProfile(BlogUsers user, HttpPostedFileBase ProfileImage)
        {
            ModelState.Remove("ModifiedUser");
           if (ModelState.IsValid) 
           {
                if (ProfileImage != null && (ProfileImage.ContentType == "image/png" ||
                                      ProfileImage.ContentType == "image/jpg" ||
                                      ProfileImage.ContentType == "image/jpeg"))
                {
                    string filename = $"user_{user.Id}.{ProfileImage.ContentType.Split('/')[1]}";
                    ProfileImage.SaveAs(Server.MapPath($"~/images/{filename}"));
                    user.ProfileImage = filename;
                }

                BusinessResult<BlogUsers> _result = blogUserManager.UpdateProfile(user);

                if (_result.Errors.Count > 0)
                {
                    ModelState.AddModelError("", "Username or Email is already taken");
                    return View(user);
                }

                Session["login"] = _result.Result;

                return RedirectToAction("ShowProfile");
            }
            return View(user);
        }

        [Authority]
        public ActionResult DeleteProfile()
        {
            BlogUsers user = Session["login"] as BlogUsers;

            BusinessResult<BlogUsers> _result = blogUserManager.DeleteUser(user.Id);

            if(_result.Errors.Count > 0)
            {
                ModelState.AddModelError("", "Error");
                return RedirectToAction("/Home/ShowProfile");
            }
 
            Session.Clear();
            return RedirectToAction("Index"); ;
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult AccessError()
        {
            return View();
        }
    }
}