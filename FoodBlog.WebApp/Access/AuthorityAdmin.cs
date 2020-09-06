using FoodBlog.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodBlog.WebApp.Access
{
    public class AuthorityAdmin : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (UserSession.User != null && UserSession.User.IsAdmin == false)
            {
                filterContext.Result = new RedirectResult("/Home/AccessError");
            }
        }
    }
}