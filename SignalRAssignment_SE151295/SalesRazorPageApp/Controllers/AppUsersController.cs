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
    public class AppUsersController : Controller
    {
        public IUserRepository repository = new UserRepository();

        [HttpGet]
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("ErrorSession", "Login");
            }

            List<AppUsers> appUsers = repository.GetUserList();
            return View(appUsers);
        }


        // GET: AppUsers/Create
        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("ErrorSession", "Login");
            }
            return View();
        }

         [HttpPost]
         [ValidateAntiForgeryToken]
         public  IActionResult Create([Bind("UserID,FullName,Address,Email,Password")] AppUsers appUsers)
         {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("ErrorSession", "Login");
            }
           
            if (ModelState.IsValid)
             {
                 repository.AddUser(appUsers);
                TempData["SuccessMessage"] = "Create successfully";
                return RedirectToAction(nameof(Index));
             }
            
            return View(appUsers);
         }

        // GET: AppUsers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("ErrorSession", "Login");
            }

            if (id == null)
            {
                return NotFound();
            }

            var user = repository.GetUsersById((int)id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int UserID, [Bind("UserID,FullName,Address,Email,Password")] AppUsers appUsers)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("ErrorSession", "Login");
            }

            if (UserID != appUsers.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TempData["SuccessMessage"] = "Edit successfully";
                    repository.UpdateUser(appUsers);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {

                }

                //return RedirectToAction("EditUser",new { id = appUsers.UserID});
            }

            return View(appUsers);
        }

        // GET: AppUsers/Edit/5
        public IActionResult EditUser(int? userId)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("ErrorSession", "Login");
            }
             var user = repository.GetUsersById((int)userId);

            if (user == null)
            {
                user = repository.GetCurrentMemberLogin();
            }

            ViewBag.UserID = user.UserID;

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        
    

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(int UserID, [Bind("UserID,FullName,Address,Email,Password")] AppUsers appUsers)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("ErrorSession", "Login");
            }

            if (UserID != appUsers.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TempData["SuccessMessage"] = "Edit successfully";
                    repository.UpdateUser(appUsers);
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                
                //return RedirectToAction("EditUser",new { id = appUsers.UserID});
            }
            
            return View(appUsers);
        }

        // GET: AppUsers/Delete/5
        public IActionResult Delete(int? id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("ErrorSession", "Login");
            }

            if (id == null)
            {
                return NotFound();
            }

            var user = repository.GetUsersById((int)id);

             if (user == null)
             {
                 return NotFound();
             }

            return View(user);
        }

        // POST: AppUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int UserID)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("ErrorSession", "Login");
            }

            var appUsers = repository.GetUsersById((int)UserID);
            repository.DeleteUser(appUsers);
            TempData["SuccessMessage"] = "Delete successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
