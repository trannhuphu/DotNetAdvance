using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        public IActionResult OnGetSession()
        {
            ErrorMsg = "Please login first!!";
            return Page();
        }

        public string ErrorMsg {set;get;}

        public async Task<IActionResult> OnPostAsync()
        {
            IConfiguration config = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, true)
                         .Build();
            
            string userAdmin = config["Account:email"];
            string userPassword = config["Account:password"];

            if (CusLogin.Email == userAdmin && CusLogin.Password == userPassword)
            {
                 HttpContext.Session.SetString("username", userAdmin);
                return RedirectToPage("/Customers/CustomerManage");
            }

            Customer CusTmp = await _context.Customers.FirstOrDefaultAsync(m => m.Email == CusLogin.Email 
                && m.Password == CusLogin.Password);

            if(CusTmp == null)
            {
                ErrorMsg = "Email or Password  are incorrect";
                return Page();
            }

            HttpContext.Session.SetString("username", CusTmp.ContactName);

            return RedirectToPage("/Customers/UserInfo", "User",new {id = CusTmp.CustomerId});
        }
    }
}