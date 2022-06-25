using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;

namespace SignalRAssignment.Pages.Products
{
    public class EditModel : PageModel
    {
        private IProductRepository productRepository = new ProductRepository();

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public string ErrMsg { get; set; }

        public  IActionResult OnGet(int? id)
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

            ViewData["CategoryId"] = new SelectList(productRepository.GetCategories(), "CategoryId", "CategoryName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                productRepository.UpdatePro(Product);
            }
            catch
            {
                ErrMsg = "Can not update product!";
                return Page();
            }

            return RedirectToPage("./MainProduct");
        }
    }
}
