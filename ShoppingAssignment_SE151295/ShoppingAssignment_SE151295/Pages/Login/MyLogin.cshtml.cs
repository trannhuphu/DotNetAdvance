using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoppingAssignment_SE151295.Models;

namespace ShoppingAssignment_SE151295.Pages.Login
{
    public class MyLoginModel : PageModel
    {
        private readonly ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext _context;
       
        public MyLoginModel(NorthwindCopyDBContext context)
        {
            _context = context;
        }

        [BindProperty (SupportsGet = true)]
        public Customer CusLogin { get; set; }

        public void OnGet()
        {
        }

        public string ErrorMsg {set;get;}

        public async Task<IActionResult> OnPostAsync()
        {
            Customer CusTmp = await _context.Customers.FirstOrDefaultAsync(m => m.Email == CusLogin.Email 
                && m.Password == CusLogin.Password);

            if(CusTmp == null)
            {
                ErrorMsg = "Email or Passowrd are incorrect";
                return Page();
            }

            return RedirectToPage("/Customers/Index");
        }
    }
}