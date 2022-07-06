using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class PostDAO
    {
        private static PostDAO instance = null;
        private static readonly object instanceLock = new object();
        public PostDAO() { }
        public static PostDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if(instance == null)
                    {
                        instance = new PostDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Posts> GetPostList()
        {
            var db = new ApplicationDBContext();
            List<Posts> posts = db.Posts.Include(p => p.PostCategories)
                                        .Include(u => u.AppUsers)
                                        .ToList();
            return posts;
        }

        public Posts GetPostById(int postId)
        {
            var db = new ApplicationDBContext();
            Posts post = db.Posts.Include(p => p.PostCategories)
                                 .Include(u => u.AppUsers)
                                 .Where(tmp => tmp.PostID == postId).FirstOrDefault();
            return post;
        }

        public void CreatePost(Posts post)
        {
            var db = new ApplicationDBContext();
            db.Posts.Add(post);
            db.SaveChanges();
        }
        public void UpdatePost(Posts post)
        {
            var db = new ApplicationDBContext();
            db.Entry<Posts>(post).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeletePost(Posts post)
        {
            var db = new ApplicationDBContext();
            db.Posts.Remove(post);
            db.SaveChanges();
        }
        public List<PostCategories> GetCategoryList()
        {
            var db = new ApplicationDBContext();
            List<PostCategories> category = db.PostCategories.ToList();
            return category;
        }

        public List<AppUsers> GetAppUserList()
        {
            var db = new ApplicationDBContext();
            List<AppUsers> appUsers = db.AppUsers.ToList();
            return appUsers;
        }

    }
}
