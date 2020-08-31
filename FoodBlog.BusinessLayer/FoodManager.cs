using FoodBlog.DAL.EntityFramework;
using FoodBlog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodBlog.BusinessLayer
{
    public class FoodManager
    {
        private Repository<Food> repo_food = new Repository<Food>();

        public List<Food> GetFoods()
        {
            return repo_food.List();
        } 
    }
}
