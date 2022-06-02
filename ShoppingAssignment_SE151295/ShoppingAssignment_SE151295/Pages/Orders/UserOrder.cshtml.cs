using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task OnGetAsync()
        {
            Order = await _context.Orders
                .Where(p=> p.CustomerId == UserCurrent.CustomerId).ToListAsync();
        }
    }
}
