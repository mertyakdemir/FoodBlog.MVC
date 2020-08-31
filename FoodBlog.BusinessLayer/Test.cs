//using FoodBlog.DAL.EntityFramework;
//using FoodBlog.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FoodBlog.BusinessLayer
//{
//    public class Test
//    {
//        private Repository<Category> repo_category = new Repository<Category>();
//        private Repository<BlogUsers> repo_user = new Repository<BlogUsers>();
//        private Repository<Comment> repo_comment = new Repository<Comment>();
//        private Repository<Food> repo_food = new Repository<Food>();

//        public Test()
//        {
//            List<Category> categories = repo_category.List();
//        }

//        public void AddTest()
//        {
//            int result = repo_user.Insert(new BlogUsers()
//            {
//                Name = "Yiğit",
//                Surname = "Demir",
//                Username = "MehmetY",
//                Email = "mehmetdemir@hotmail.com",
//                Password = "123456",
//                IsActive = true,
//                IsAdmin = true,
//                ActiveGuid = Guid.NewGuid(),
//                Created = DateTime.Now,
//                Modified = DateTime.Now.AddHours(3),
//                ModifiedUser = "YiğitD",
//            });
//        }

//        public void UpdateTest()
//        {
//            BlogUsers user = repo_user.Find(x => x.Name == "Yiğit");

//            if( user != null)
//            {
//                user.Name = "UpdateYiğit";
//                int result = repo_user.Update(user);
//            }
//        }

//        public void DeleteTest()
//        {
//            BlogUsers user = repo_user.Find(x => x.Name == "UpdateYiğit");

//            if (user != null)
//            {
//                int result = repo_user.Delete(user);
//            }
//        }
//        public void CommentTest()
//        {
//            BlogUsers user = repo_user.Find(x => x.Id == 3);
//            Food food = repo_food.Find(x => x.Id == 1);

//            Comment comment = new Comment()
//            {
//                Text = "Test Comment Update",
//                Created = DateTime.Now,
//                Modified = DateTime.Now,
//                ModifiedUser = "MertY",
//                Food = food,
//                Owner = user
//            };
//            repo_comment.Insert(comment);
//        }
//    }
//}
