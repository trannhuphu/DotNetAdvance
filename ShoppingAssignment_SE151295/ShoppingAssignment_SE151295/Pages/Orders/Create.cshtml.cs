using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingAssignment_SE151295.Models;

namespace ShoppingAssignment_SE151295.Pages.Orders
{

    public class CreateModel : PageModel
    {
        private readonly ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext _context;

        public CreateModel(ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToPage("/Login/MyLogin","Session");
            }

            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
                return Page();
        }

        [BindProperty]
        public Order Order { get; set; }

        [BindProperty]
        public string ErrorMessage {set;get;}

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Order orderTmp =  _context.Orders.Where(p => p.OrderId == Order.OrderId)
                                .SingleOrDefault();

            if(orderTmp != null)
            {
                ErrorMessage = "This order with id = " + Order.OrderId + " has already exist!!";
                ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
                return Page();
            }
            else
            {
                Order.OrderDate = DateTime.Now;
                _context.Orders.Add(Order);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./OrderManage");
        }
    }
}
