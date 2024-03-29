﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreIdentity.Models;
using Microsoft.AspNetCore.Authorization;

namespace CoreIdentity.Controllers
{

  
    public class HomeController : Controller
    {


   
        public IActionResult Index()
        {
            return View();
        }




        [Authorize(Roles = "Admin")]
        public IActionResult Sample()
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
