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

namespace ShoppingAssignment_SE151295.Pages.Products
{
    public class UpLoadFilesModel : PageModel
    {
        public string Message { get; set; }
        private IHostingEnvironment _environment;
        public UpLoadFilesModel(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        [Required(ErrorMessage = "Please choose at least on file")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "png, jpg, jpeg, gif")]
        [Display(Name = "Choose file(s) to upload")]
        [BindProperty]

        public IFormFile[] FileUploads { get; set; }
        public string Clicked()
        {
            var clickMessage = "I was Clicked";
            return clickMessage;
        }
        public async Task OnPostAsync()
        {
            Message = "";
            if (FileUploads != null)
            {
                foreach (var FileUpload in FileUploads)
                {
                    var file = Path.Combine(_environment.ContentRootPath, "Images", FileUpload.FileName);
                    using (var fileStream = new FileStream(file, FileMode.Create))
                    {
                        await FileUpload.CopyToAsync(fileStream);
                        Message = "The file was upload successfully";
                    }
                }
            }
        }
    }
}
