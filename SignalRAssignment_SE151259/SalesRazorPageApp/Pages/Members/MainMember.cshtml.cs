using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;

namespace SignalRAssignment.Pages.Members
{
    public class MainMemberModel : PageModel
    {
        private IMemberRepository memberRepository = new MemberRepository();

        public IList<Member> Member { get;set; }

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToPage("/Index", "Session");
            }

            bool IsAdmin = memberRepository.CheckIsAdmin();
            if(IsAdmin == true)
            {
                Member = memberRepository.GetMembers();
            }
            else
            {
                int memId = memberRepository.GetCurrentMemberId();
                return RedirectToPage("Edit", new {id = memId});
            }    

            return Page();
        }
    }
}
