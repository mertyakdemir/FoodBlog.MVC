using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoodBlog.BusinessLayer;
using FoodBlog.Entities;
using FoodBlog.WebApp.Access;
using FoodBlog.WebApp.Models;

namespace FoodBlog.WebApp.Controllers
{
    [ErrorPage]
    public class FoodController : Controller
    {
        private FoodManager foodManager = new FoodManager();
        private CategoryManager categoryManager = new CategoryManager();
        private LikeManager likeManager = new LikeManager();

        // GET: Food
        [Authority]
        public ActionResult Index()
        {
            var foods = foodManager.ListQueryable().Include("Category").Include("Owner").Where(x => x.Owner.Id == UserSession.User.Id).OrderByDescending(x => x.Modified);
            return View(foods.ToList());
        }

        // GET: Food/Details/5
        [Authority]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = foodManager.Find(x => x.Id == id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // GET: Food/Create
        [Authority]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(categoryManager.List(), "Id", "Title");
            return View();
        }

        [Authority]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Food food, HttpPostedFileBase FoodImage)
        {
            ModelState.Remove("ModifiedUser");
            ModelState.Remove("Created");
            ModelState.Remove("Modified");

            if (ModelState.IsValid)
            {
                if (FoodImage != null && (FoodImage.ContentType == "image/png" ||
                                     FoodImage.ContentType == "image/jpg" ||
                                     FoodImage.ContentType == "image/jpeg"))
                {
                    string filename = $"food_{food.Id}.{FoodImage.ContentType.Split('/')[1]}";
                    FoodImage.SaveAs(Server.MapPath($"~/images/{filename}"));
                    food.FoodImage = filename;
                }

                food.Owner = UserSession.User;
                foodManager.Insert(food);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(categoryManager.List(), "Id", "Title", food.CategoryId);
            return View(food);
        }

        // GET: Food/Edit/5
        [Authority]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = foodManager.Find(x => x.Id == id);
            if (food == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(categoryManager.List(), "Id", "Title", food.CategoryId);
            return View(food);
        }

        [Authority]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Food food, HttpPostedFileBase FoodImage)
        {
            ModelState.Remove("ModifiedUser");
            ModelState.Remove("Created");
            ModelState.Remove("Modified");

            if (ModelState.IsValid)
            {
                if (FoodImage != null && (FoodImage.ContentType == "image/png" ||
                                    FoodImage.ContentType == "image/jpg" ||
                                    FoodImage.ContentType == "image/jpeg"))
                {
                    string filename = $"food_{food.Id}.{FoodImage.ContentType.Split('/')[1]}";
                    FoodImage.SaveAs(Server.MapPath($"~/images/{filename}"));
                    food.FoodImage = filename;
                }

                Food food_update = foodManager.Find(x => x.Id == food.Id);
                food_update.Title = food.Title;
                food_update.Text = food.Text;
                food_update.CategoryId = food.CategoryId;
                food_update.IsDraft = food.IsDraft;
                food_update.FoodImage = food.FoodImage;

                foodManager.Update(food_update);

                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(categoryManager.List(), "Id", "Title", food.CategoryId);
            return View(food);
        }

        // GET: Food/Delete/5
        [Authority]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = foodManager.Find(x => x.Id == id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Food/Delete/5
        [Authority]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Food food = foodManager.Find(x => x.Id == id);
            foodManager.Delete(food);
            return RedirectToAction("Index");
        }

        [Authority]
        public ActionResult MyLikedFoods()
        {
            var foods = likeManager.ListQueryable().Include("LikedUsers").Include("Food").Where(
                x => x.LikedUsers.Id == UserSession.User.Id).Select(
                x => x.Food).Include("Category").Include("Owner").OrderByDescending(
                x => x.Modified);

            return View("Index", foods.ToList());
        }

        [Authority]
        [HttpPost]
        public ActionResult GetFavorites(int[] ids)
        {
            if (UserSession.User != null)
            {
                int userId = UserSession.User.Id;
                List<int> favFoodIds = new List<int>();

                if (ids != null)
                {
                    favFoodIds = likeManager.List(x => x.LikedUsers.Id == userId && ids.Contains(x.Food.Id)).Select(x => x.Food.Id).ToList();
                }

                else
                {
                    favFoodIds = likeManager.List(x => x.LikedUsers.Id == userId).Select(x => x.Food.Id).ToList();
                }

                return Json(new { result = favFoodIds });
            }
            else
            {
                return Json(new { result = new List<int>() });
            }
        }

        [HttpPost]
        public ActionResult SetLike(int card_id, bool like)
        {
            int _result = 0;

            if (UserSession.User == null)
                return Json(new { anyError = true, errorMessage = "Please login", result = 0 });


            Like liked = likeManager.Find(x => x.Food.Id == card_id && x.LikedUsers.Id == UserSession.User.Id);

            Food food = foodManager.Find(x => x.Id == card_id);

            if (liked != null && like == false)
            {
                _result = likeManager.Delete(liked);
            }
            else if (liked == null && like == true)
            {
                _result = likeManager.Insert(new Like()
                {
                    LikedUsers = UserSession.User,
                    Food = food
                });
            }

            if (_result > 0)
            {
                _result = foodManager.Update(food);

                return Json(new { anyError = false, errorMessage = string.Empty });
            }

            return Json(new { anyError = true, errorMessage = "Error" });
        }
    }
}
