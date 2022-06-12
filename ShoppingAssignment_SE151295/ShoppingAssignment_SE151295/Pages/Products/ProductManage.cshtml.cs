using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoppingAssignment_SE151295.Models;

namespace ShoppingAssignment_SE151295.Pages.Products
{
    public class ProductManageModel : PageModel
    {
        private readonly ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext _context;

        public ProductManageModel(ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext context)
        {
            _context = context;
        }

 

        public IList<Product> Product { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToPage("/Login/MyLogin","Session");
            }

            Product = await _context.Products
                .Include(p => p.OrderDetails)
                .Include(p => p.Category)
                .Include(p => p.Supplier).ToListAsync();

            foreach(var item in Product)
            {
                if(item.OrderDetails.Count != 0)
                {
                    item.ProductStatus = 0;
                    _context.Update(item);
                }
                else
                {
                    if (item.ProductStatus == 0)
                    {
                        item.ProductStatus = 1;
                        _context.Update(item);
                    }
                }
            }

            _context.SaveChanges();
            return Page();
        }

        [BindProperty]
        public string search { set; get; }
        public IActionResult OnPostSearchProduct()
        {
            if (!string.IsNullOrEmpty(search))
            {
                Product = _context.Products.Where(p => p.ProductId == int.Parse(search)).ToList();
            }

            return Page();
        }
    }
}
