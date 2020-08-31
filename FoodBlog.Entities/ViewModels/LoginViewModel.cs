using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodBlog.Entities.ViewModels
{
    public class LoginViewModel
    {
        [DisplayName("Username"), Required(ErrorMessage = "{0} cannot be empty.")]
        public string Username { get; set; }

        [DisplayName("Password"), Required(ErrorMessage = "{0} cannot be empty."), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}