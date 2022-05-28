using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingAssignment_SE151295.Models;

namespace ShoppingAssignment_SE151295.Pages.Login
{
    public class MyLoginModel : PageModel
    {
        private readonly ShoppingAssignment_SE151295.Models.NorthwindCopyDBContext _DBcontext;
       
        public MyLoginModel(NorthwindCopyDBContext DBcontext)
        {
            _DBcontext = DBcontext;
        }

        [BindProperty (SupportsGet = true)]
        public string MyName {set; get;}

        public void OnGet()
        {
         
        }
    }
}
