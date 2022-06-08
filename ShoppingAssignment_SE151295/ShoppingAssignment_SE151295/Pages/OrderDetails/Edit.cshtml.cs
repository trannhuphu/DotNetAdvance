using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingAssignment_SE151295.Models;

namespace ShoppingAssignment_SE151295.Pages.OrderDetails
{
    public class EditModel : PageModel
    {
        private readonly ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext _context;

        public EditModel(ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; }

        [BindProperty]
        public string ErrorMsg {set;get;}

        [BindProperty]
        public int oldQuanity {set;get;}

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderDetail = await _context.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Product).FirstOrDefaultAsync(m => m.OrderId == id);

            if (OrderDetail == null)
            {
                return NotFound();
            }
            oldQuanity = OrderDetail.Quantity;
           ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
           ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(OrderDetail).State = EntityState.Modified;

            try
            {
                int shiftQuantity = OrderDetail.Quantity - oldQuanity;

                Product product = _context.Products.Where(p => p.ProductId == OrderDetail.ProductId).SingleOrDefault();

                product.QuantityPerUnit = product.QuantityPerUnit - shiftQuantity;
                if (product.QuantityPerUnit < 0)
                {
                    ErrorMsg = product.ProductName + " is not enough to buy";
                    return Page();
                }

                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(OrderDetail.OrderId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./OrderDetailManage",new {id = OrderDetail.OrderId});
        }

        private bool OrderDetailExists(string id)
        {
            return _context.OrderDetails.Any(e => e.OrderId == id);
        }
    }
}
