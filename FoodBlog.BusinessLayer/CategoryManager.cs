using FoodBlog.DAL.EntityFramework;
using FoodBlog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodBlog.BusinessLayer
{
    public class CategoryManager : MainManager<Category>
    {
        //public override int Delete(Category category)
        //{
        //    FoodManager foodManager = new FoodManager();
        //    LikeManager likeManager = new LikeManager();
        //    CommentManager commentManager = new CommentManager();

        //    foreach (Food food in category.Foods.ToList())
        //    {
        //        foreach (Like like in food.Likes.ToList())
        //        {
        //            likedManager.Delete(like);
        //        }

        //        foreach (Comment comment in food.Comments.ToList())
        //        {
        //            commentManager.Delete(comment);
        //        }

        //        foodManager.Delete(food);
        //    }

        //    return base.Delete(category);
        //}
    }
}
