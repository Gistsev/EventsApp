using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRegister.Business.Models;
using TestRegister.DAL.Entity;

namespace TestRegister.Business
{
    public class UserHelper
    {
        private static SessionModelSingleton _singleton;


        public UserHelper(SessionModelSingleton singltone)
        {
            _singleton = singltone;
        }

        public bool changeLang(string lang)
        {
            try
            {
                Tools.setUpLocale(lang);

                var user = getUser();
                user.lang = lang;
                _singleton.repo.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;

        }
        public UserModel UserInfo(int? k)
        {
            

            var model = new UserModel();
            var user = _singleton.repo.Users.Where(u => u.Id == k).First();

            model.user = new UserPreview();
            model.user.imgUrl = user.imgURL;
            model.user.UserName = Tools.getName(user);
            model.user.UserID = user.Id;
            model.user.Email = user.Email;
            model.user.Name = user.Name;
            model.user.SecondName = user.Surname;
            model.user.LastName = user.LastName;
            model.user.Phone = user.PhoneNumber;


            //var posts = repo.getUserPosts(id);
       

            return model;
        }
        public User getUser()
        {
            try
            {
                return _singleton.repo.Users.First(m => m.Id == _singleton.userId);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public User GetUser(string email)
        {
            try
            {
                return _singleton.repo.Users.First(u => u.Email == email);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
