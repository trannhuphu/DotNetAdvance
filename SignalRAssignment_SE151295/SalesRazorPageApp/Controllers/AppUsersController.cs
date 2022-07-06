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
    public class AppUsersController : Controller
    {
        public IUserRepository repository = new UserRepository();

        [HttpGet]
        public IActionResult Index()
        {
            bool IsMemberLogin = repository.CheckIsMemberLogin();
            if (IsMemberLogin)
            {
                return RedirectToAction("Edit", new { id = repository.
                    GetCurrentMemberLogin().UserID});
            }

            List<AppUsers> appUsers = repository.GetUserList();
            return View(appUsers);
        }

        // GET: AppUsers/Create
        public IActionResult Create()
        {
            return View();
        }

         [HttpPost]
         [ValidateAntiForgeryToken]
         public  IActionResult Create([Bind("UserID,FullName,Address,Email,Password")] AppUsers appUsers)
         {
             if (ModelState.IsValid)
             {
                 repository.AddUser(appUsers);
                 return RedirectToAction(nameof(Index));
             }
             return View(appUsers);
         }

        // GET: AppUsers/Edit/5
        public IActionResult Edit(int? id)
        {
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
        public IActionResult Edit(int id, [Bind("UserID,FullName,Address,Email,Password")] AppUsers appUsers)
        {
            if (id != appUsers.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    repository.UpdateUser(appUsers);
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction(nameof(Index));
            }
            return View(appUsers);
        }

        // GET: AppUsers/Delete/5
        public IActionResult Delete(int? id)
        {
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
            var appUsers = repository.GetUsersById((int)UserID);
            repository.DeleteUser(appUsers);
            return RedirectToAction(nameof(Index));
        }
    }
}
