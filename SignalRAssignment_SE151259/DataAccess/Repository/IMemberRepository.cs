using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        List<Member> GetMembers();
        Member GetMemByID(int memId);
        void AddMem(Member member);
        void Delete(Member member);
        void UpdateMem(Member member);
        void Login(string email, string password);
        bool CheckIsAdmin();
        int GetCurrentMemberId();
    }
}
