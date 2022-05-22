using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class MemberDAO
    {

        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
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
            List<Member> mem;
            try
            {
                var myMemDB = new FStoreDBContext();
                mem = myMemDB.Members.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return mem;
        }

        public Member GetMemByID(int memID)
        {
            Member mem = null;
            try
            {
                var myMemDB = new FStoreDBContext();
                mem = myMemDB.Members.SingleOrDefault(mem => mem.MemberId == memID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return mem;
        }

        public void AddMember(Member mem)
        {
            try
            {
                Member member = GetMemByID(mem.MemberId);
                if (member == null)
                {
                    var myMemDB = new FStoreDBContext();
                    myMemDB.Members.Add(mem);
                    myMemDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The member is already exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateMember(Member mem)
        {
            try
            {
                Member mb = GetMemByID(mem.MemberId);
                if (mb != null)
                {
                    var myMemDB = new FStoreDBContext();
                    myMemDB.Entry<Member>(mem).State = EntityState.Modified;
                    myMemDB.SaveChanges();
                }
                 else
                 {
                     throw new Exception("The member does not already exist");
                 }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveMember(Member mem)
        {
            try
            {
                Member member = GetMemByID(mem.MemberId);
                if (member != null)
                {
                    var myMemDB = new FStoreDBContext();
                    myMemDB.Members.Remove(mem);
                    myMemDB.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
                throw new Exception ("User mail or Password is incorrect!");
            }
        }
    }
}

 
