using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoppingAssignment_SE151295.Models;

namespace ShoppingAssignment_SE151295.Pages.Customers
{
    public class CustomerManageModel : PageModel
    {
        private readonly ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext _context;

        public CustomerManageModel(ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext context)
        {
            _context = context;
        }

        public IList<Customer> Customer { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToPage("/Login/MyLogin","Session");
            }

            Customer = await _context.Customers.ToListAsync();
            return Page();
        }
    }
}
