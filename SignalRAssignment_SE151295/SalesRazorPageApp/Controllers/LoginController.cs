﻿using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesRazorPageApp.Controllers
{
    public class LoginController : Controller
    {
        public IUserRepository userRepository = new UserRepository();
        public IActionResult CheckLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckLogin(string email, string password)
        {
            try
            {
                bool IsLoginSuccess = false;
                IsLoginSuccess = userRepository.checkLogin(email, password);
                if (IsLoginSuccess)
                {
                    ViewBag.IsMemberLogin = false;
                    return RedirectToAction("Index", "Posts");
                }

                ViewBag.IsMemberLogin = true;
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Input Invalid";
            }

            ViewBag.Message = "Email or Password is incorrect";
            return View();
        }
    }
}