using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UserRepository:IUserRepository
    {
        /*public List<Posts> GetPostList()=>UserDAO.Instance.GetPostList();*/
        public void DeleteUser(AppUsers user) => UserDAO.Instance.DeleteUser(user);
        public void UpdateUser(AppUsers user) => UserDAO.Instance.UpdateUser(user);
        public void AddUser(AppUsers user) => UserDAO.Instance.AddUser(user);
        public AppUsers GetUsersById(int userId) => UserDAO.Instance.GetUsersById(userId);
        public List<AppUsers> GetUserList() => UserDAO.Instance.GetUserList();
        public bool checkLogin(string email, string password) => UserDAO.Instance.checkLogin(email, password);
        public AppUsers GetCurrentMemberLogin() => UserDAO.Instance.GetCurrentMemberLogin();
        public bool CheckIsMemberLogin() => UserDAO.Instance.CheckIsMemberLogin();
    }
}
