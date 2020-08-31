using System;
using System.Collections.Generic;
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
        [Required, StringLength(40)]
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual List<Food> Foods { get; set; }
        public Category()
        {
            Foods = new List<Food>();
        }
    }
}
