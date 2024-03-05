using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Services;
using LanguageExchangeHub1.Services.Base;
using LanguageExchangeHub1.Services.Contracts;
using LanguageExchangeHub1.Services.Models;
using LanguageExchangeHub1.Services.Models.Courses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LanguageExchangeHub1.Controllers
{
    public class CourseController : Controller
    {
        public readonly ICourseService _courseService;
        public readonly ILanguageService _languageService;
        public readonly IUserService _userService;
        private readonly IUserData _userData;
        private readonly ICourseUserService _courseUserService;
        private readonly IRequestService _requestService;

        public CourseController(ICourseService courseService, ILanguageService languageService,
            IUserService userService, IUserData userData, ICourseUserService courseUserService,
            IRequestService requestService)
        {
            _courseService = courseService;
            _languageService = languageService;
            _userService = userService;
            _userData = userData;
            _courseUserService = courseUserService;
            _requestService = requestService;
        }

        
        public async Task<IActionResult> CreateCourse()
        {
          
            var languagaes = await _languageService.GetAllAsync();
            IEnumerable enumerableList = languagaes;
            ViewBag.Languages = enumerableList;
         
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCourse(CourseViewModel courseViewModel)
        {
           if (ModelState.IsValid)
            {
                if (courseViewModel.Image != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        courseViewModel.ImageFile.CopyTo(memoryStream);
                        courseViewModel.Image = memoryStream.ToArray();
                    }
                }
            }
            Course course = await _courseService.CreateAsync(courseViewModel);

            return RedirectToAction("Index", "Home",course);
        }

        public async Task<IActionResult> AddMember()
        {
            return View();
        }


        public async Task<IActionResult> CourseChat(string courseId)
        {
            CourseViewModel course = await _courseService.GetAsync(courseId);
           
           
            return View(course);
        }
        [HttpPost]
        public async Task<IActionResult> Request(string courseId)
        {
            
            
            var request = await _requestService.CreateAsync(courseId);
           
           
            return View("Index");
        }
    }
}

