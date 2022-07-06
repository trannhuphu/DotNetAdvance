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

        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public List<AppUsers> GetUserList()
        {

            var applicationDBContext = repository.GetUserList();
            return applicationDBContext;
        }

        // GET: AppUsers/Create
        public IActionResult Create()
        {
            return View();
        }
        /* [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create([Bind("UserID,FullName,Address,Email,Password")] AppUsers appUsers)
         {
             if (ModelState.IsValid)
             {
                 _context.Add(appUsers);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }
             return View(appUsers);
         }*/

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

        /*   [HttpPost]
           [ValidateAntiForgeryToken]
           public async Task<IActionResult> Edit(int id, [Bind("UserID,FullName,Address,Email,Password")] AppUsers appUsers)
           {
               if (id != appUsers.UserID)
               {
                   return NotFound();
               }

               if (ModelState.IsValid)
               {
                   try
                   {
                       _context.Update(appUsers);
                       await _context.SaveChangesAsync();
                   }
                   catch (DbUpdateConcurrencyException)
                   {
                       if (!AppUsersExists(appUsers.UserID))
                       {
                           return NotFound();
                       }
                       else
                       {
                           throw;
                       }
                   }
                   return RedirectToAction(nameof(Index));
               }
               return View(appUsers);
           }*/

        // GET: AppUsers/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = repository.GetUsersById((int)id);

            /* if (posts == null)
             {
                 return NotFound();
             }*/

            return View(user);
        }
/*
        // POST: AppUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appUsers = await _context.AppUsers.FindAsync(id);
            _context.AppUsers.Remove(appUsers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/

       
    }
}
