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
                            .Where(p => p.ProductId == Product.ProductId)
                            .SingleOrDefault();
            
            if(tmpPro != null)
            {
                ErrorMessage = "The Product with id = " + tmpPro.ProductId + " has already exist!";
                return Page();
            }
            else
            {
                _context.Products.Add(Product);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./ProductManage");
        }

        public string Message { get; set; }
        

        [Required(ErrorMessage = "Please choose at least on file")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "png, jpg, jpeg, gif")]
        [Display(Name = "Choose file(s) to upload")]
        [BindProperty]
        public IFormFile FileUpload { get; set; }
        public string Clicked()
        {
            var clickMessage = "I was Clicked";
            return clickMessage;
        }

        public string imageName { set; get; }
        public IActionResult OnPostHello()
        {
            Message = "";
            if (FileUpload != null)
            {
            
                    var file = Path.Combine(_environment.WebRootPath, "images", FileUpload.FileName);
                    using (var fileStream = new FileStream(file, FileMode.Create))
                    {
                        FileUpload.CopyTo(fileStream);
            
                        Message = "The file was upload successfully";
                    }            
            }
            return Page();
        }
    }
}
