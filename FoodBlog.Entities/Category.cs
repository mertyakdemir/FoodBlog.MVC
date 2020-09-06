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
    [Table("Categories")]
    public class Category : EntityBase
    {
        [DisplayName("Category"), Required, StringLength(30, ErrorMessage = "The {0} field must be a maximum of {1} characters.")]
        public string Title { get; set; }
        [DisplayName("Description"), StringLength(60)]
        public string Description { get; set; }
        public virtual List<Food> Foods { get; set; }
        public Category()
        {
            Foods = new List<Food>();
        }
    }
}
