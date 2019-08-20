using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreIdentity.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {

        private UserManager<AplicationUser> userManger;
        private SignInManager<AplicationUser> signinManger;

        public AccountController(UserManager<AplicationUser> _usermanger, SignInManager<AplicationUser> _signinManger)
        {
            
           

            userManger = _usermanger;
            signinManger = _signinManger;
        }


        public IActionResult Index()
        {
            return View();
        }


        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Login(LoginModel model,  string returnUrl)
        {

            if (ModelState.IsValid)
            {
                var user = await userManger.FindByEmailAsync(model.Email);


                if (user !=null)
                {
                    await signinManger.SignOutAsync();
                    var result = await signinManger.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }

                ModelState.AddModelError("Email","İnvalid Email or Password");

            }
            return View(model);
        }


    }
}