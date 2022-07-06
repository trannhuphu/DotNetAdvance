using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserDAO
    {
        private static UserDAO instance = null;
        private static readonly object instanceLock = new object();
        public UserDAO() { }
       public static UserDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if(instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }
        public List<AppUsers> GetUserList()
        {
            var db = new ApplicationDBContext();
            List<AppUsers> user = db.AppUsers.ToList();
            return user;
        }
        public AppUsers GetUsersById(int userId)
        {
            var db = new ApplicationDBContext();
            AppUsers user = db.AppUsers.Where(tmp => tmp.UserID == userId).FirstOrDefault();
            return user;
        }
        public void AddUser(AppUsers user)
        {
            var db = new ApplicationDBContext();
            db.AppUsers.Add(user);
            db.SaveChanges();
        }
        public void UpdateUser(AppUsers user)
        {
            var db = new ApplicationDBContext();
            db.Entry<AppUsers>(user).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void DeleteUser(AppUsers user)
        {
            var db = new ApplicationDBContext();
            db.AppUsers.Remove(user);
            db.SaveChanges();
        }
        /*public List<Posts> GetPostList()
        {
            var db = new ApplicationDBContext();
            List<Posts> post = db.Posts.ToList();
            return post;
        }*/

        public AppUsers CurrentMemberLogin { set; get; }

        public AppUsers GetCurrentMemberLogin() => CurrentMemberLogin;

        public bool IsMemberLogin { set; get; }

        public bool CheckIsMemberLogin() => IsMemberLogin;

        public bool checkLogin(string email, string password)
        {
            IConfiguration config = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, true)
                        .Build();

            string userAdmin = config["Account:email"];
            string userPassword = config["Account:password"];

            if (userAdmin == email && userPassword == password)
            {
                IsMemberLogin = false;
                return true;
            }

            var db = new ApplicationDBContext();
            AppUsers appUsers = db.AppUsers.Where(p => p.Email == email
                                && p.Password == password).FirstOrDefault();
            if (appUsers != null)
            {
                IsMemberLogin = true;
                CurrentMemberLogin = appUsers;
                return true;
            }

            return false;
        }
    }
}
