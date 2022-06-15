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

namespace ShoppingAssignment_SE151295.Pages.Orders
{
    public class UserOrderModel : CommonUser
    {
        private readonly ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext _context;
        public UserOrderModel(ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; }

        [BindProperty]
        public Customer Customer {get;set;} = UserCurrent;

        public async Task<IActionResult> OnGetAsync()
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToPage("/Login/MyLogin","Session");
            }

            Order = await _context.Orders
                .Where(p=> p.CustomerId == UserCurrent.CustomerId).ToListAsync();
            
            return Page();
        }
    }
}
