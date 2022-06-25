using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;

namespace SignalRAssignment.Pages.Members
{
    public class CreateModel : PageModel
    {

        public IActionResult OnGet()
        {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToPage("/Index", "Session");
            }

            return Page();
        }

        [BindProperty]
        public string ErrMsg { get; set; }

        [BindProperty]
        public Member Member { get; set; }

        private IMemberRepository memberRepository = new MemberRepository();

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                memberRepository.AddMem(Member);
            }
            catch
            {
                ErrMsg = "Can not create member";
                return Page();
            }

            return RedirectToPage("./MainMember");
        }
    }
}
