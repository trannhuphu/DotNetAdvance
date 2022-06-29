using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserDAO
    {
        private static UserDAO instance = null;
        private static readonly object instanceLock = null;
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
    }
}
