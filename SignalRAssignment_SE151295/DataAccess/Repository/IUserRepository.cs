using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IUserRepository
    {
        /*List<Posts> GetPostList();*/
        void DeleteUser(AppUsers user);
        void UpdateUser(AppUsers user);
        void AddUser(AppUsers user);
        AppUsers GetUsersById(int userId);
        List<AppUsers> GetUserList();

    }
}
