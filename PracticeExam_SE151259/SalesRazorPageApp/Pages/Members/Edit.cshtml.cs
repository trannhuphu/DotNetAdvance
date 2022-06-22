using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;

namespace SalesRazorPageApp.Pages.Members
{
    public class EditModel : PageModel
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                memberRepository.UpdateMem(Member);
            }
            catch
            {
                ErrMsg = "Can not update member!";
            }

            return RedirectToPage("./MainMember");
        }
    }
}
