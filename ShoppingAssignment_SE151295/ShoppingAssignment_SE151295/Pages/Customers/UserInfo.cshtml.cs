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

namespace ShoppingAssignment_SE151295.Pages.Customers
{
    public class UserInfoModel : CommonUser
    {
        
        private readonly ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext _context;

        public UserInfoModel(ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; } = UserCurrent;

        public bool IsUpdateSuccess { get; set; } = false;

        public async Task<IActionResult> OnGetUserAsync(string id)
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToPage("/Login/MyLogin","Session");
            }
            
            if (id == null)
            {
                return NotFound();
            }
            
            return Page();
        }

        public IActionResult OnGetView()
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToPage("/Login/MyLogin","Session");
            }
            
            if(UserCurrent != null)
            {
                Customer = UserCurrent;
            }
            else
            {
                return NotFound();
            }          
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

            _context.Attach(Customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(Customer.CustomerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            UserCurrent = Customer;
            IsUpdateSuccess = true;
            return Page();
        }

        private bool CustomerExists(string id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
