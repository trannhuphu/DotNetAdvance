using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingAssignment_SE151295.Models;

namespace ShoppingAssignment_SE151295.Pages.Customers
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

            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; }

        [BindProperty]
        public string ErrorMessage { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Customer Custemp = null;
            try
            {
                Custemp = _context.Customers.Where(p => p.Email.Contains(Customer.Email)
                                || p.CustomerId.Contains(Customer.CustomerId)).SingleOrDefault();
            }
            catch(Exception ex)
            {
                ErrorMessage = "The User ID or Email has already exist!";
                return Page();
            }

            if(Custemp != null)
            {
                ErrorMessage = "The User ID or Email has already exist!";
                return Page();
            }
            else
            {
                TempData["SuccessMessage"] = "Create successfully";
                _context.Customers.Add(Customer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./CustomerManage");
        }
    }
}
