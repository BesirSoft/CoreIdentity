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

        private IPasswordValidator<AplicationUser> paswordValidator;

        private IPasswordHasher<AplicationUser> paswordHassher;

        public AdminController(UserManager<AplicationUser> _userManger, IPasswordValidator<AplicationUser> _paswordValidator, IPasswordHasher<AplicationUser> _paswordHassher)
        {
            userManger = _userManger;
            paswordValidator = _paswordValidator;
            paswordHassher = _paswordHassher;

        }
        public IActionResult Index()
        {

            return View(userManger.Users);
        }

        [HttpGet]
        public IActionResult Create()
        {


            return View();
        }


        [HttpPost]
        public   async Task< IActionResult > Create(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                AplicationUser user = new AplicationUser();
                user.UserName = model.UserName;
                user.Email = model.Email;
                var result = await userManger.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {

                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("",item.Description);
                    } 
                   
                }

            }


            return View(model);
        }

        [HttpPost]
        public  async Task < IActionResult> Delete( string id)
        {
            var user = await userManger.FindByIdAsync(id);
            if (user !=null)
            {
                var result = await userManger.DeleteAsync(user);



                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {

                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("",item.Description);
                    }

                }

            }
            else
            {

                ModelState.AddModelError("", "user not found");


            }

            return View("Index", userManger.Users);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string Id)
        {
            var user = await userManger.FindByIdAsync(Id);

            if (user !=null)
            {
                return View(user);
            }
            else
            {

                return RedirectToAction("Index");

            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(string Id, string Password, string Email )
        {
            var user = await userManger.FindByIdAsync(Id);

            if (user != null)
            {

                user.Email = Email;
                IdentityResult validPass=null;
                if (!string.IsNullOrEmpty(Password))
                {
                    validPass = await paswordValidator.ValidateAsync(userManger,user, Password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = paswordHassher.HashPassword(user, Password);


                    }
                    else
                    {
                        foreach (var item in validPass.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }

                    }
                 }


                if (validPass.Succeeded)
                {

                    var result = await userManger.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var item in validPass.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }

                }

            }
            else
            {
                ModelState.AddModelError("","user NotFound");
            }

            return View(user);
        }

    }
}