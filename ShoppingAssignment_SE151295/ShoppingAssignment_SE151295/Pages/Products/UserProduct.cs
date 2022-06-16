using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoppingAssignment_SE151295.Models;
using ShoppingAssignment_SE151295.Pages.Customers;

namespace ShoppingAssignment_SE151295.Pages.Products
{
    public class UserProductModel : CommonUser
    {
        private readonly ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext _context;

        public UserProductModel(ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        [BindProperty]
        public Customer Customer {get;set;}

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToPage("/Login/MyLogin","Session");
            }

            Customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);
            if(Customer != null)
            {
                UserCurrent = Customer;
            }
            else
            {
                if(UserCurrent != null)
                {
                    Customer = UserCurrent;
                }
                else
                {
                    return NotFound();
                }
            }
           
            Product = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier).ToListAsync();

            return Page();
        }
    }
}
