using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataAccess
{
    public class MemberDAO
    {

        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }
        public static MemberDAO INSTANCE
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Member> GetMemberList()
        {
            var myMemDB = new FStoreDBContext();
            List<Member> mem = myMemDB.Members.ToList();  
            return mem;
        }

        public Member GetMemByID(int memID)
        {
            var myMemDB = new FStoreDBContext();
            Member mem = myMemDB.Members.SingleOrDefault(mem => mem.MemberId == memID);  
            return mem;
        }

        public void AddMember(Member mem)
        {
           var myMemDB = new FStoreDBContext();
           myMemDB.Members.Add(mem);
           myMemDB.SaveChanges();
        }

        public void UpdateMember(Member mem)
        {
           Member mb = GetMemByID(mem.MemberId);
           var myMemDB = new FStoreDBContext();
           myMemDB.Entry<Member>(mem).State = EntityState.Modified;
           myMemDB.SaveChanges();
        }

        public void RemoveMember(Member mem)
        {
           Member member = GetMemByID(mem.MemberId);
           var myMemDB = new FStoreDBContext();
           myMemDB.Members.Remove(mem);
           myMemDB.SaveChanges();
        }

        private int CurrentMemberId = 0;
        public int GetCurrentMemberId() => CurrentMemberId;

        public int Login(string strEmail, string strPassword, ref Member member)
        {

            IConfiguration config = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", true, true)
                                     .Build();

            string userAdmin = config["Account:email"];
            string userPassword = config["Account:password"];

            if (strEmail == userAdmin && strPassword == userPassword)
            {
                return 1; //role admin
            }
            else
            {
                member = GetMemberList().Where(tmp => tmp.Email == strEmail
                                        && tmp.Password == strPassword).FirstOrDefault();
                if (member != null)
                {
                    CurrentMemberId = member.MemberId;
                    return 2;
                }
                throw new Exception("User mail or Password is incorrect!");
            }
        }
    }
}
