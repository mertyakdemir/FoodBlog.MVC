using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoodBlog.BusinessLayer;
using FoodBlog.Entities;
using FoodBlog.WebApp.Access;

namespace FoodBlog.WebApp.Controllers
{
    [Authority]
    [AuthorityAdmin]
    [ErrorPage]
    public class BlogUserController : Controller
    {
        private BlogUserManager blogUserManager = new BlogUserManager();

        // GET: BlogUser
        public ActionResult Index()
        {
            return View(blogUserManager.List());
        }

        // GET: BlogUser/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BlogUsers blogUsers = blogUserManager.Find(x => x.Id == id.Value);
            if (blogUsers == null)
            {
                return HttpNotFound();
            }
            return View(blogUsers);
        }

        // GET: BlogUser/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogUsers blogUsers)
        {
            ModelState.Remove("ModifiedUser");
            ModelState.Remove("Created");
            ModelState.Remove("Modified");

            if (ModelState.IsValid)
            {
                BusinessResult<BlogUsers> _result = blogUserManager.Insert(blogUsers);
                if(_result.Errors.Count > 0)
                {
                    ModelState.AddModelError("", "Username or Email is already taken");
                    return View(blogUsers);

                }
                return RedirectToAction("Index");
            }

            return View(blogUsers);
        }

        // GET: BlogUser/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogUsers blogUsers = blogUserManager.Find(x => x.Id == id.Value);

            if (blogUsers == null)
            {
                return HttpNotFound();
            }
            return View(blogUsers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BlogUsers blogUsers)
        {
            ModelState.Remove("ModifiedUser");
            ModelState.Remove("Created");
            ModelState.Remove("Modified");

            if (ModelState.IsValid)
            {
                BusinessResult<BlogUsers> _result = blogUserManager.Update(blogUsers);
                if (_result.Errors.Count > 0)
                {
                    ModelState.AddModelError("", "Error");
                    return View(blogUsers);

                }
                return RedirectToAction("Index");
            }
            return View(blogUsers);
        }

        // GET: BlogUser/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogUsers blogUsers = blogUserManager.Find(x => x.Id == id.Value);
            if (blogUsers == null)
            {
                return HttpNotFound();
            }
            return View(blogUsers);
        }

        // POST: BlogUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogUsers blogUsers = blogUserManager.Find(x => x.Id == id);
            blogUserManager.Delete(blogUsers);
            return RedirectToAction("Index");
        }
    }
}
