using FoodBlog.Common;
using FoodBlog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodBlog.WebApp
{
    public class WebCommon : ICommon
    {
        public string GetUsername()
        {
            if(HttpContext.Current.Session["login"] != null)
            {
                BlogUsers user = HttpContext.Current.Session["login"] as BlogUsers;
                return user.Username;
            }
            return "system";
        }
    }
}