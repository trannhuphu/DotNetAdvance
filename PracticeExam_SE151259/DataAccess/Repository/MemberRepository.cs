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
        public Member GetMemByID(int memId) => MemberDAO.Instance.GetMemByID(memId);
        public IEnumerable<Member> GetMembers() => MemberDAO.Instance.GetMemberList();
        public void AddMem(Member member) => MemberDAO.Instance.AddMember(member);
        public void Delete(Member member) => MemberDAO.Instance.RemoveMember(member);
        public void UpdateMem(Member member) => MemberDAO.Instance.UpdateMember(member);

        public int Login(string email, string password, ref Member member) => MemberDAO.Instance.Login(email, password, ref member);
    }
}
