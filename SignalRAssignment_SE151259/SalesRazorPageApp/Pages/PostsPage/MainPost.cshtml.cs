using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;

namespace SignalRAssignment.Pages.PostsPage
{
    public class MainPostModel : PageModel
    {

        public IList<Posts> Posts { get;set; }

        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
