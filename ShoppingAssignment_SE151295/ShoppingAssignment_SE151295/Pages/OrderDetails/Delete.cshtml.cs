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
    public class DeleteModel : PageModel
    {
        private readonly ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext _context;

        public DeleteModel(ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; }

        public async Task<IActionResult> OnGetAsync(string orderid, int productid)
        {
            if (orderid == null)
            {
                return NotFound();
            }

            OrderDetail = await _context.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Product)
                .Where(m => m.OrderId == orderid && m.ProductId == productid)
                .FirstOrDefaultAsync();

            if (OrderDetail == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
           // if (id == null)
           // {
           //     return NotFound();
           // }

            //OrderDetail = await _context.OrderDetails.FindAsync(id);

            string orderid = "";

            if (OrderDetail != null)
            {
                orderid = OrderDetail.OrderId;
                _context.OrderDetails.Remove(OrderDetail);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./OrderDetailManage",new {id = orderid});
        }
    }
}
