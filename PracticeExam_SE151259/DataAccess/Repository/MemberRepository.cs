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
        public IEnumerable<Member> GetMembers() => MemberDAO.INSTANCE.GetMemberList();
        public void AddMem(Member member) => MemberDAO.INSTANCE.AddMember(member);
        public void Delete(Member member) => MemberDAO.INSTANCE.RemoveMember(member);
        public void UpdateMem(Member member) => MemberDAO.INSTANCE.UpdateMember(member);

        public int Login(string email, string password, ref Member member) => MemberDAO.INSTANCE.Login(email, password, ref member);
    }
}
