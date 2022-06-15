using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoppingAssignment_SE151295.Models;

namespace ShoppingAssignment_SE151295.Pages.Orders
{
    public class DeleteModel : PageModel
    {
        private readonly ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext _context;

        public DeleteModel(ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToPage("/Login/MyLogin","Session");
            }
            
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Orders
                .Include(o => o.Customer).FirstOrDefaultAsync(m => m.OrderId == id);

            if (Order == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Orders.FindAsync(id);

            if (Order != null)
            {
                IList<OrderDetail> listOrderDetailRm = _context.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Product)
                .Where(p => p.OrderId == Order.OrderId).ToList();

                foreach (var tmepOrderDetail in listOrderDetailRm)
                {
                    int shiftQuantity = tmepOrderDetail.Quantity;

                    Product product = _context.Products.
                    Where(p => p.ProductId == tmepOrderDetail.ProductId).SingleOrDefault();

                    product.QuantityPerUnit = product.QuantityPerUnit + shiftQuantity;
                    _context.Update(product);
                }

                _context.OrderDetails.RemoveRange(listOrderDetailRm);
                _context.Orders.Remove(Order);

                await _context.SaveChangesAsync();
            }
            TempData["SuccessMessage"] = "Delete successfully";
            return RedirectToPage("./OrderManage");
        }
    }
}
