using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingAssignment_SE151295.Models;

namespace ShoppingAssignment_SE151295.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext _context;
        private IWebHostEnvironment _environment;

        public CreateModel(ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToPage("/Login/MyLogin","Session");
            }
 
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "CompanyName");
            return Page();
        }

/*         [BindProperty]
        public int LocalProductId { get; set; } */

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public string ErrorMessage { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostCreateAsync()
        {
            
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "CompanyName");
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Product tmpPro = _context.Products
                            .Where(p => p.ProductName.ToLower().Trim() == Product.ProductName.ToLower().Trim())
                            .SingleOrDefault();
            
            if(tmpPro != null)
            {
                ErrorMessage = "The Product with name = " + tmpPro.ProductName + " has already exist!";
                return Page();
            }
            else
            {
                if(!string.IsNullOrEmpty(Product.ProductImage))
                {
                    string FilePath = Path.Combine(_environment.WebRootPath,
                    "images", Product.ProductImage);
                    System.IO.File.Delete(FilePath);
                } 

                Product.ProductImage = ProcessUploadImageFile();

                _context.Products.Add(Product);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./ProductManage");
        }
        
        [BindProperty]
        public IFormFile FileUpload { get; set; }
        public string ProcessUploadImageFile()
        {
            if (FileUpload != null)
            {
            
                    var file = Path.Combine(_environment.WebRootPath, "images", FileUpload.FileName);
                    using (var fileStream = new FileStream(file, FileMode.Create))
                    {
                        FileUpload.CopyTo(fileStream);
                    }            
            }
            return FileUpload.FileName;
        }
    }
}
