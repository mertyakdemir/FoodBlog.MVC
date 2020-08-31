using FoodBlog.Common.Helpers;
using FoodBlog.DAL.EntityFramework;
using FoodBlog.Entities;
using FoodBlog.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodBlog.BusinessLayer
{
    public class BlogUserManager
    {
        private Repository<BlogUsers> repo_user = new Repository<BlogUsers>();

        public BusinessResult<BlogUsers> RegisterUser(RegisterViewModel user)
        {
            BlogUsers userE = repo_user.Find(x => x.Username == user.Username || x.Email == user.Email);
            BusinessResult<BlogUsers> _result = new BusinessResult<BlogUsers>();

            if( userE != null)
            {
                if(userE.Username == user.Username )
                {
                    _result.Errors.Add("This username is already registered, please choose another one.");
                }
                if (userE.Email == user.Email)
                {
                    _result.Errors.Add("This email is already registered, please choose another one.");
                }
            }

            else
            {
                int dbRes = repo_user.Insert(new BlogUsers()
                {
                    Username = user.Username,
                    Email = user.Email,
                    Password = user.Password,
                    ActiveGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false,
                });
                if(dbRes > 0)
                {
                    _result.Result = repo_user.Find(x => x.Username == user.Username && x.Email == user.Email);

                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}/Home/ActivateUser/{_result.Result.ActiveGuid}";
                    string body = $"Hello {_result.Result.Username}, please click on the <a href='{activateUri}' target='_blank'> to activate your account.";
                    MailHelper.SendMail(body, _result.Result.Email, "Activation Link");
                }
            }

            return _result;
        }

        public BusinessResult<BlogUsers> LoginUser(LoginViewModel user)
        {
            BusinessResult<BlogUsers> _result = new BusinessResult<BlogUsers>();
            _result.Result = repo_user.Find(x => x.Username == user.Username && x.Password == user.Password);

            if(_result.Result != null)
            {
                if(!_result.Result.IsActive)
                {
                    _result.Errors.Add("The user is not activated. Please check your e-mail address.");
                }
            }
            else
            {
                _result.Errors.Add("Username and password do not match.");
            }
            return _result;
        }

        public BusinessResult<BlogUsers> ActivateUser(Guid guid)
        {
            BusinessResult<BlogUsers> _result = new BusinessResult<BlogUsers>();
            _result.Result = repo_user.Find(x => x.ActiveGuid == guid);

            if(_result.Result != null)
            {
                if(_result.Result.IsActive)
                {
                    _result.Errors.Add("The user has already been active.");
                    return _result;
                }
                _result.Result.IsActive = true;
                repo_user.Update(_result.Result);
            }
            else
            {
                _result.Errors.Add("The user to activate was not found.");
            }
            return _result;
        }

        public BusinessResult<BlogUsers> GetUser(int id)
        {
            BusinessResult<BlogUsers> _result = new BusinessResult<BlogUsers>();
            _result.Result = repo_user.Find(x => x.Id == id);

            if(_result.Result == null)
            {
                _result.Errors.Add("User not found");
            }
            return _result;
        }
    }
}
