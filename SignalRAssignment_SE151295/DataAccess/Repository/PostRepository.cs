using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class PostRepository:IPostRepository
    {
        public List<PostCategories> GetCategoryList()=>PostDAO.Instance.GetCategoryList();
        public void DeletePost(Posts post) => PostDAO.Instance.DeletePost(post);
        public void UpdatePost(Posts post) => PostDAO.Instance.UpdatePost(post);
        public void CreatePost(Posts post) => PostDAO.Instance.CreatePost(post);
        public Posts GetPostById(int postId) => PostDAO.Instance.GetPostById(postId);
        public List<Posts> GetPostList() => PostDAO.Instance.GetPostList();
        public List<AppUsers> GetAppUserList() => PostDAO.Instance.GetAppUserList();
        public List<Posts> SearchPost(string search) => PostDAO.Instance.SearchPost(search);
    }
}
