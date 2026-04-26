using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Services.Models;
using LanguageExchangeHub1.Services.Models.Courses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using LanguageExchangeHub1.Services.Contracts;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LanguageExchangeHub1.Controllers
{
    public class RolesController : Controller
    {

        public readonly IRoleService _roleService;


        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;

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
                await _roleService.CreateAsync(roleViewModel);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}







