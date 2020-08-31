using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodBlog.Entities.ViewModels
{
    public class RegisterViewModel
    {
        [DisplayName("Username"), Required(ErrorMessage = "{0} cannot be empty.")]
        public string Username { get; set; }

        [DisplayName("Email"), Required(ErrorMessage = "{0} cannot be empty."), EmailAddress(ErrorMessage = "Enter a valid {0} for the {0} field")]
        public string Email { get; set; }

        [DisplayName("Password"), Required(ErrorMessage = "{0} cannot be empty."), DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Password again"), Required(ErrorMessage = "{0} cannot be empty."), DataType(DataType.Password),
        Compare("Password", ErrorMessage = "{0} does not match the {1}")]
        public string ResPassword { get; set; }
    }
}