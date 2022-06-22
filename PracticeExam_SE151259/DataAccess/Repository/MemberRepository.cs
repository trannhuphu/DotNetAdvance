using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public Member GetMemByID(int memId) => MemberDAO.INSTANCE.GetMemByID(memId);
        public List<Member> GetMembers() => MemberDAO.INSTANCE.GetMemberList();
        public void AddMem(Member member) => MemberDAO.INSTANCE.AddMember(member);
        public void Delete(Member member) => MemberDAO.INSTANCE.RemoveMember(member);
        public void UpdateMem(Member member) => MemberDAO.INSTANCE.UpdateMember(member);
        public void Login(string email, string password) => MemberDAO.INSTANCE.Login(email, password);
        public bool CheckIsAdmin() => MemberDAO.INSTANCE.CheckIsAdmin();
        public int GetCurrentMemberId() => MemberDAO.INSTANCE.GetCurrentMemberId();
    }
}
