﻿using Microsoft.AspNetCore.Mvc;

namespace ShoppingCart.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        
        public IActionResult Logout()
        {
            return View();
        }
        
        public IActionResult Register()
        {
            return View();
        }
    }
}
