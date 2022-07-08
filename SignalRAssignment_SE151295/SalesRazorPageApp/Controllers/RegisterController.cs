using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;

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
          /*  if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("ErrorSession", "Login");
            }*/

            return View();
        }

        // POST: Register/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("UserID,FullName,Address,Email,Password")] AppUsers appUsers)
        {
           /* if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("ErrorSession", "Login");
            }*/

            if (ModelState.IsValid)
            {
                              
                repository.AddUser(appUsers);
                ViewBag.IsUpdateSuccess = true;
                //    return RedirectToAction("CheckLogin", "Login");
             //   return RedirectToAction(nameof(Create));
            }
            return View();
        }

       
    }
}
