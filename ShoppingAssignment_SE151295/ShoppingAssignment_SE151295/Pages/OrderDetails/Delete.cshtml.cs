using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        [BindProperty]
        public int oldQuanity {set;get;}

        public async Task<IActionResult> OnGetAsync(string orderid, int productid)
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToPage("/Login/MyLogin","Session");
            }

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
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToPage("/Login/MyLogin","Session");
            }

            string orderid = "";

            if (OrderDetail != null)
            {
                orderid = OrderDetail.OrderId;

                OrderDetail tmepOrderDetail =  _context.OrderDetails
                            .Include(o => o.Order)
                            .Include(o => o.Product)
                            .Where(p => p.OrderId == OrderDetail.OrderId && p.ProductId == OrderDetail.ProductId)
                            .SingleOrDefault();

                int shiftQuantity = tmepOrderDetail.Quantity;
                Product product = _context.Products.Where(p => p.ProductId == OrderDetail.ProductId).SingleOrDefault();
                product.QuantityPerUnit = product.QuantityPerUnit + shiftQuantity;
                _context.Update(product);
                _context.OrderDetails.Remove(OrderDetail);
                await _context.SaveChangesAsync();
            }
            TempData["SuccessMessage"] = "Delete successfully";
            return RedirectToPage("./OrderDetailManage",new {id = orderid.Trim()});
        }
    }
}
