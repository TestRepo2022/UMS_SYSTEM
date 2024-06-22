﻿using APP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace APP.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}