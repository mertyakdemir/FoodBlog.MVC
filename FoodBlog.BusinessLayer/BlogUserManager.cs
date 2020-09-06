using FoodBlog.Common.Helpers;
using FoodBlog.DAL.EntityFramework;
using FoodBlog.Entities;
using FoodBlog.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FoodBlog.BusinessLayer
{
    public class BlogUserManager : MainManager<BlogUsers>
    {
        public BusinessResult<BlogUsers> RegisterUser(RegisterViewModel user)
        {
            BlogUsers userE = Find(x => x.Username == user.Username || x.Email == user.Email);
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
                int dbRes = base.Insert(new BlogUsers()
                {
                    Username = user.Username,
                    Email = user.Email,
                    Password = user.Password,
                    ActiveGuid = Guid.NewGuid(),
                    ProfileImage = "profile.png",
                    IsActive = false,
                    IsAdmin = false,
                }); ;
                if(dbRes > 0)
                {
                    _result.Result = Find(x => x.Username == user.Username && x.Email == user.Email);

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
            _result.Result = Find(x => x.Username == user.Username && x.Password == user.Password);

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
            _result.Result = Find(x => x.ActiveGuid == guid);

            if(_result.Result != null)
            {
                if(_result.Result.IsActive)
                {
                    _result.Errors.Add("The user has already been active.");
                    return _result;
                }
                _result.Result.IsActive = true;
                Update(_result.Result);
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
            _result.Result = Find(x => x.Id == id);

            if(_result.Result == null)
            {
                _result.Errors.Add("User not found");
            }
            return _result;
        }

        public BusinessResult<BlogUsers> UpdateProfile(BlogUsers user)
        {
            BlogUsers blogUser = Find(x => x.Id != user.Id && (x.Username == user.Username || x.Email == user.Email));
            BusinessResult<BlogUsers> _result = new BusinessResult<BlogUsers>();
            
                if(blogUser != null && blogUser.Id != user.Id)
                {
                    if(blogUser.Username == user.Username)
                    {
                        _result.Errors.Add("User already registered");
                    }
                    if (blogUser.Email == user.Email)
                    {
                        _result.Errors.Add("Email already registered");
                    }
                    return _result;
                }
                _result.Result = Find(x => x.Id == user.Id);
                _result.Result.Username = user.Username;
                _result.Result.Password = user.Password;
                _result.Result.Email = user.Email;
                _result.Result.Name = user.Name;
                _result.Result.Surname = user.Surname;

                if (string.IsNullOrEmpty(user.ProfileImage) == false)
                {
                    _result.Result.ProfileImage = user.ProfileImage;
                }

                if (base.Update(_result.Result) == 0)
                {
                    _result.Errors.Add("Profile not updated");
                }

                return _result;
        }

        public BusinessResult<BlogUsers> DeleteUser(int id)
        {
            BusinessResult<BlogUsers> _result = new BusinessResult<BlogUsers>();
            BlogUsers user = Find(x => x.Id == id);

            if(user != null)
            {
                if(Delete(user) == 0)
                {
                    _result.Errors.Add("User could not be deleted");
                    return _result;
                }
            }
            else
            {
                _result.Errors.Add("User not found");
            }

            return _result;
        }

        public new BusinessResult<BlogUsers> Insert(BlogUsers user)
        {
            BlogUsers userE = Find(x => x.Username == user.Username || x.Email == user.Email);
            BusinessResult<BlogUsers> _result = new BusinessResult<BlogUsers>();

            _result.Result = user;

            if (userE != null)
            {
                if (userE.Username == user.Username)
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
                _result.Result.ProfileImage = "profile.png";
                _result.Result.ActiveGuid = Guid.NewGuid();

                if(base.Insert(_result.Result) == 0)
                {
                    _result.Errors.Add("User could not be added.");
                }
            }

            return _result;
        }

        public new BusinessResult<BlogUsers> Update(BlogUsers user)
        {
            BlogUsers blogUser = Find(x => x.Id != user.Id && (x.Username == user.Username || x.Email == user.Email));
            BusinessResult<BlogUsers> _result = new BusinessResult<BlogUsers>();
            _result.Result = user;

            if (blogUser != null && blogUser.Id != user.Id)
            {
                if (blogUser.Username == user.Username)
                {
                    _result.Errors.Add("User already registered");
                }
                if (blogUser.Email == user.Email)
                {
                    _result.Errors.Add("Email already registered");
                }
                return _result;
            }
            _result.Result = Find(x => x.Id == user.Id);
            _result.Result.Username = user.Username;
            _result.Result.Password = user.Password;
            _result.Result.Email = user.Email;
            _result.Result.Name = user.Name;
            _result.Result.Surname = user.Surname;
            _result.Result.IsActive = user.IsActive;
            _result.Result.IsAdmin = user.IsAdmin;

            if (base.Update(_result.Result) == 0)
            {
                _result.Errors.Add("Profile not updated");
            }

            return _result;
        }
    }
}
