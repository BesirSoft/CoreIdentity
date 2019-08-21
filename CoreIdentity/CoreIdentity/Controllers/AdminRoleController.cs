using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreIdentity.Controllers
{
    public class AdminRoleController : Controller
    {

        private RoleManager<IdentityRole> rolManager;

        public AdminRoleController(RoleManager<IdentityRole> _rolManager)
        {
            rolManager = _rolManager;
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