using Biz.Models.Models.Roles;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace LanguageExchangeHub1.Controllers
{
    public class RolesController : Controller
    {
        public readonly IRoleService roleService;
        
        public RolesController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
       
        public async Task<IActionResult> CreateRole(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                await roleService.CreateAsync(roleViewModel);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}