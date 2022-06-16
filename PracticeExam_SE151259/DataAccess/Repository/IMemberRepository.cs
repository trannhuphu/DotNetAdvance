﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetMembers();
        Member GetMemByID(int memId);
        void AddMem(Member member);
        void Delete(Member member);
        void UpdateMem(Member member);
        int Login(string email, string password, ref Member member);
    }
}
