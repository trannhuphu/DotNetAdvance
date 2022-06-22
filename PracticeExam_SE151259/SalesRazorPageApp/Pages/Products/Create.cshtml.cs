using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;

namespace SalesRazorPageApp.Pages.Products
{
    public class CreateModel : PageModel
    {
        private IProductRepository productRepository = new ProductRepository();
        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToPage("/Index", "Session");
            }
            ViewData["CategoryId"] = new SelectList(productRepository.GetCategories(), "CategoryId", "CategoryName");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public string ErrMsg { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ViewData["CategoryId"] = new SelectList(productRepository.GetCategories(), "CategoryId", "CategoryName");

            try
            {
                productRepository.AddPro(Product);
            }
            catch
            {
                ErrMsg = "The product has already exist!";
                return Page();
            }

            return RedirectToPage("./MainProduct");
        }
    }
}
