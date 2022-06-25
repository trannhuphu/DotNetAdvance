using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRAssignment.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public Member member { set; get; }

        [BindProperty]
        public string ErrMsg { set; get; }

        public IActionResult OnGet()
        {
            HttpContext.Session.Clear();
            return Page();
        }
        public IActionResult OnGetSession()
        {
            ErrMsg = "Please login first!!";
            return Page();
        }

        private IMemberRepository memberRepository = new MemberRepository();

        public IActionResult OnPost()
        {
            try
            {
                memberRepository.Login(member.Email, member.Password);
                HttpContext.Session.SetString("username", member.Email);
                return RedirectToPage("./Products/MainProduct");       
            }
            catch
            {
                ErrMsg = "Email or Password is incorrect!";
            }

            return Page();
        }
    }
}
