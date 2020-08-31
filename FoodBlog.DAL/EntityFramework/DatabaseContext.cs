using FoodBlog.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodBlog.DAL.EntityFramework
{
    public class DatabaseContext : DbContext
    {
       public DbSet<BlogUsers> BlogUsers { get; set; }
       public DbSet<Food> Foods { get; set; }
       public DbSet<Comment> Comments { get; set; }
       public DbSet<Category> Categories { get; set; }
       public DbSet<Like> Likes { get; set; }
       
       public DatabaseContext()
       {
           Database.SetInitializer(new Initializer());
       }
    }
}
