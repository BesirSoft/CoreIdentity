using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreIdentity.Controllers
{
    public class AdminRoleController : Controller
    {

        private RoleManager<IdentityRole> rolManager;
        private UserManager<AplicationUser> userManager;

        public AdminRoleController(RoleManager<IdentityRole> _rolManager, UserManager<AplicationUser> _userManager)
        {
            rolManager = _rolManager;
            userManager = _userManager;
        }


        public IActionResult Index()
        {
            return View(rolManager.Roles);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> Create(string Rolname)
        {

            if (ModelState.IsValid)
            {
                var result = await rolManager.CreateAsync(new IdentityRole(Rolname));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                }

            }
            return View(Rolname);
        }



        public async Task<IActionResult> Edit(string id)
        {

            IdentityRole role = await rolManager.FindByIdAsync(id);

            var mebers = new List<AplicationUser>();
            var nonmebers = new List<AplicationUser>();


            foreach (var user in userManager.Users)
            {
                var list = await userManager.IsInRoleAsync(user, role.Name)    ? mebers: nonmebers;



                list.Add(user);

            }

            var model = new RoleDetails()

            {
                Role = role,
                Mebers = mebers,
                NonMebers = nonmebers



            };
            
            return View(model);
        }








        [HttpPost]

        public async Task< IActionResult> Delete(string id)
        {
            var role = await rolManager.FindByIdAsync(id);



            if (role !=null)
            {
                var result = await rolManager.DeleteAsync(role);


                if (result.Succeeded)
                {

                    TempData["message"] = $"{role} has been delete.";

                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

             

            }


            return RedirectToAction("Index");






        }



    }
}