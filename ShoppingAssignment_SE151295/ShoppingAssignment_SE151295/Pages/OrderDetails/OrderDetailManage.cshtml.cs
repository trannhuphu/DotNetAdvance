using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoppingAssignment_SE151295.Models;

namespace ShoppingAssignment_SE151295.Pages.OrderDetails
{
    public class OrderDetailManageModel : PageModel
    {
        private readonly ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext _context;

        public OrderDetailManageModel(ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext context)
        {
            _context = context;
        }

        public IList<OrderDetail> OrderDetail { get;set; }

        public string OrderId {get;set;}

        public decimal totalPrice {get;set;} = 0;
        public decimal Price {get;set;} = 0;

        public async Task OnGetAsync(string id)
        {
            OrderDetail = await _context.OrderDetails
                .Where(o => o.OrderId == id)
                .Include(o => o.Order)
                .Include(o => o.Product).ToListAsync();

            OrderId = id;
        }
    }
}
