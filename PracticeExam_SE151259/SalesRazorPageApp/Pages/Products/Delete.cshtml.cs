using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;

namespace SalesRazorPageApp.Pages.Products
{
    public class DeleteModel : PageModel
    {

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public string ErrMsg { get; set; }

        private IProductRepository productRepository = new ProductRepository();

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToPage("/Index", "Session");
            }

            Product = productRepository.GetProByID((int)id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                Product = productRepository.GetProByID((int)id);
                if (Product != null)
                {
                    productRepository.Delete(Product);
                }
            }
            catch
            {
                ErrMsg = "Product can not be deleted!";
            }

            return RedirectToPage("./MainProduct");
        }
    }
}
