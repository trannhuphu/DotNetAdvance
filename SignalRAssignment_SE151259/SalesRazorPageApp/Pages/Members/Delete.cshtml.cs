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
    public class DeleteModel : PageModel
    {

        [BindProperty]
        public Member Member { get; set; }

        [BindProperty]
        public string ErrMsg { get; set; }

        private IMemberRepository memberRepository = new MemberRepository();

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToPage("/Index", "Session");
            }

            Member = memberRepository.GetMemByID((int)id);

            if (Member == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                Member = memberRepository.GetMemByID((int)id);
                memberRepository.Delete(Member);
            }
            catch
            {
                ErrMsg = "Can not delete member";
            }

            return RedirectToPage("./MainMember");
        }
    }
}
