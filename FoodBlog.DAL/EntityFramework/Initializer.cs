using FoodBlog.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodBlog.DAL.EntityFramework
{
    public class Initializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            BlogUsers blogUserAdmin = new BlogUsers()
            {
                Name = "TestName",
                Surname = "TestSurname",
                Username = "TestUsername",
                Email = "test@hotmail.com",
                Password = "123456",
                ProfileImage = "profile.png",
                IsActive = true,
                IsAdmin = true,
                ActiveGuid = Guid.NewGuid(),
                Created = DateTime.Now,
                Modified = DateTime.Now.AddHours(10),
                ModifiedUser = "TestUserChanged",
            };

            BlogUsers blogUserNormal = new BlogUsers()
            {
                Name = "TestName 2",
                Surname = "TestSurname 2",
                Username = "TestUsername 2",
                Email = "test2@hotmail.com",
                Password = "123456",
                ProfileImage = "profile.png",
                IsActive = true,
                IsAdmin = true,
                ActiveGuid = Guid.NewGuid(),
                Created = DateTime.Now,
                Modified = DateTime.Now.AddHours(3),
                ModifiedUser = "Test2UserChanged",
            };

            context.BlogUsers.Add(blogUserAdmin);
            context.BlogUsers.Add(blogUserNormal);

            for (int i = 0; i < 10; i++)
            {
                BlogUsers user = new BlogUsers()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Username = $"user{i}",
                    Email = FakeData.NetworkData.GetEmail(),
                    Password = "asdasd",
                    ProfileImage = "profile.png",
                    IsActive = true,
                    IsAdmin = true,
                    ActiveGuid = Guid.NewGuid(),
                    Created = DateTime.Now,
                    Modified = DateTime.Now.AddHours(3),
                    ModifiedUser = $"user{i}",
                };
                context.BlogUsers.Add(user);
            }

            context.SaveChanges();

            List<BlogUsers> userlist = context.BlogUsers.ToList();

            // category fakedata
            for (int i = 0; i < 10; i++)
            {
                Category category = new Category()
                {
                    Title = FakeData.PlaceData.GetCountry(),
                    Description = FakeData.PlaceData.GetAddress(),
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    ModifiedUser = "TestUserChanged",
                };
                context.Categories.Add(category);

                // blog fakedata
                for (int k = 0; k < 5; k++)
                {
                    BlogUsers ownerFood = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)];

                    Food food = new Food()
                    {
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(10, 30)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 5)),
                        FoodImage = "img_1.jpg",
                        IsDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(1, 10),
                        Owner = ownerFood,
                        Created = DateTime.Now,
                        Modified = DateTime.Now.AddMinutes(10),
                        ModifiedUser = ownerFood.Username,
                    };
                    category.Foods.Add(food);

                    // comments fakedata
                    BlogUsers ownerComment = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)];

                    for (int j = 0; j < 4; j++)
                    {
                        Comment comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentence(),
                            Owner = ownerComment,
                            Created = DateTime.Now,
                            Modified = DateTime.Now.AddMinutes(5),
                            ModifiedUser = ownerComment.Username,
                        };
                        food.Comments.Add(comment);
                    }
                    // likes fakedata
                    for (int n = 0; n < food.LikeCount; n++)
                    {
                        Like likes = new Like()
                        {
                            LikedUsers = userlist[n]
                        };
                        food.Likes.Add(likes);
                    }
                }
            }
            context.SaveChanges();
        }
    }
}
