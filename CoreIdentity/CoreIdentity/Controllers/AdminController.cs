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














    }
}