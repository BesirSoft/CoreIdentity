using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreIdentity.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AplicationUser> userManger;

        public AdminController(UserManager<AplicationUser> _userManger)
        {
            userManger = _userManger;
        }
        public IActionResult Index()
        {

           

            return View(userManger.Users);
        }
    }
}