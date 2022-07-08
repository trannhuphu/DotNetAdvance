﻿using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
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

        public IActionResult ErrorSession()
        {
            ViewBag.Message = "Please login first";
            return View(nameof(CheckLogin));
        }

        public IActionResult CheckLogin()
        {
            HttpContext.Session.Clear();
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
                    HttpContext.Session.SetString("username",email);
                    return RedirectToAction("Index", "Posts");
                }

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
