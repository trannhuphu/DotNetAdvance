using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.Repository;

namespace SalesRazorPageApp.Controllers
{
    public class RegisterController : Controller
    {
        public IUserRepository repository = new UserRepository();
        
       /* public IActionResult Create()
        {
            return View();
        }
       */
        [HttpGet]
        // GET: Register/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Register/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("UserID,FullName,Address,Email,Password")] AppUsers appUsers)
        {
            if (ModelState.IsValid)
            {
                repository.AddUser(appUsers);
                return RedirectToAction(nameof(Create));
            }
            return View(appUsers);
        }

       
    }
}
