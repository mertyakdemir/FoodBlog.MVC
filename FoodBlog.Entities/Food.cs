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
    [Table("Foods")]
    public class Food : EntityBase
    {
        [DisplayName("Title of page"), Required, StringLength(50)]
        public string Title { get; set; }
        [DisplayName("Text of page"), Required, StringLength(1500)]
        public string Text { get; set; }
        [DisplayName("Draft")]
        public bool IsDraft { get; set; }
        [DisplayName("Favorites")]
        public int LikeCount { get; set; }
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        [StringLength(100), ScaffoldColumn(false)]
        public string FoodImage { get; set; }
        public virtual BlogUsers Owner { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Like> Likes { get; set; }

        public Food()
        {
            Comments = new List<Comment>();
            Likes = new List<Like>();
        }
    }
}
