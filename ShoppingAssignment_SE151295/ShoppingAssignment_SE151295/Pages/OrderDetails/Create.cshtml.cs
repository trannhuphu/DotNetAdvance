using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingAssignment_SE151295.Models;

namespace ShoppingAssignment_SE151295.Pages.OrderDetails
{
    public class CreateModel : PageModel
    {
        private readonly ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext _context;

        public CreateModel(ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<Product> ListPro {set;get;}

        public IActionResult OnGet(string id)
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToPage("/Login/MyLogin","Session");
            }
            
            OrderId = id;
            ListPro = _context.Products.ToList();
            return Page();
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; }

        [BindProperty]
        public decimal UnitPrice {get; set;} = 0;

        [BindProperty]
        public string OrderId {get;set;}

        [BindProperty]
        public string ErrorMsg {get;set;}

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ListPro = _context.Products.ToList();

            int productidlocal = int.Parse(Request.Form["selectedPro"]
                                .GetEnumerator().Current);

            OrderDetail.OrderId = OrderId;
            OrderDetail.UnitPrice = UnitPrice;
            OrderDetail.ProductId = productidlocal;

            OrderDetail orderDetailTemp = await _context.OrderDetails
                        .Include(o => o.Product)
                        .Where(p=>p.OrderId == OrderId).Where(p => p.ProductId == productidlocal)
                        .SingleOrDefaultAsync();

            if (orderDetailTemp != null)
            {
                ErrorMsg = "This order detail which has the " + orderDetailTemp.Product.ProductName + 
                " has already exist";
                ListPro = _context.Products.ToList();
                return Page();
            }
            else
            {
                int countQuantity = OrderDetail.Quantity;
                Product product = _context.Products.Where(p => p.ProductId == OrderDetail.ProductId).SingleOrDefault();
                if (countQuantity < product.QuantityPerUnit) 
                {
                    product.QuantityPerUnit -= countQuantity;
                }
                else
                {
                    ErrorMsg = product.ProductName + " is not enough to buy";
                    return Page();
                }
                _context.Update(product);
                _context.OrderDetails.Add(OrderDetail);     
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Create order detail successfully";
            return RedirectToPage("./OrderDetailManage", new {id = OrderId.Trim()});
        }
    }
}
