using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodBlog.Entities
{
    [Table("BlogUsers")]
    public class BlogUsers : EntityBase
    {
        [DisplayName("Name"), StringLength(30, ErrorMessage = "The {0} field must be a maximum of {1} characters.")]
        public string Name { get; set; }
        [DisplayName("Surname"), StringLength(30, ErrorMessage = "The {0} field must be a maximum of {1} characters.")]
        public string Surname { get; set; }
        [DisplayName("Username"), Required(ErrorMessage = "{0} is required"), StringLength(30, ErrorMessage = "The {0} field must be a maximum of {1} characters.")]
        public string Username { get; set; }
        [DisplayName("Email"), Required(ErrorMessage = "{0} is required"), StringLength(60, ErrorMessage = "The {0} field must be a maximum of {1} characters.")]
        public string Email { get; set; }
        [DataType(DataType.Password), DisplayName("Password"), Required(ErrorMessage = "{0} is required"), StringLength(30, ErrorMessage = "The {0} field must be a maximum of {1} characters.")]
        public string Password { get; set; }
        [StringLength(100), ScaffoldColumn(false)]
        public string ProfileImage { get; set; }
        [DisplayName("Is Active")]
        public bool IsActive { get; set; }
        [DisplayName("Is Admin")]
        public bool IsAdmin { get; set; }
        [Required, ScaffoldColumn(false)]
        public Guid ActiveGuid { get; set; }
        public virtual List<Food> Foods { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Like> Likes { get; set; }

    }
}
