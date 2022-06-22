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

namespace ShoppingAssignment_SE151295.Pages.OrderDetails
{
    public class UserOrderDetailModel : CommonUser
    {
        private readonly ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext _context;

        public UserOrderDetailModel(ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext context)
        {
            _context = context;
        }

        public IList<OrderDetail> OrderDetail { get;set; }

        [BindProperty]
        public Customer Customer {get;set;} =  UserCurrent;
        public decimal totalPrice { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToPage("/Login/MyLogin","Session");
            }
            
            OrderDetail = await _context.OrderDetails
                .Where(p => p.OrderId == id)
                .Include(o => o.Order)
                .Include(o => o.Product)
                .ToListAsync();

            return Page();
        }
    }
}
