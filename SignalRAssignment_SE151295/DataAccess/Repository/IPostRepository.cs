using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IPostRepository
    {
        List<PostCategories> GetCategoryList();
        void DeletePost(Posts post);
        void UpdatePost(Posts post);
        void CreatePost(Posts post);
        Posts GetPostById(int postId);
        List<Posts> GetPostList();
        List<AppUsers> GetAppUserList();
        List<Posts> SearchPost(string search);

    }
}
