using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LanguageExchangeHub1.Models;
using Newtonsoft.Json;
using Services.Contracts;
using Biz.Models.Models.Index;
using Microsoft.AspNetCore.Authorization;

namespace LanguageExchangeHub1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICourseService courseService;
        private readonly ILanguageService languageService;

        public HomeController(
            ICourseService courseService,
            ILanguageService languageService)
        {
            this.courseService = courseService;
            this.languageService = languageService;
        }



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index(string courseName = null, int languageId = -1)
        {
            var indexViewModel = new IndexViewModel();
            indexViewModel.Courses = await courseService.GetAllForCurrentUserAsync();
            indexViewModel.Languages = await languageService.GetAllAsync();
            if (!string.IsNullOrWhiteSpace(courseName) || languageId > 0)
            {
                indexViewModel.FilteredCourses = await courseService.GetCoursesByNameAndLangAsync(courseName, languageId);
            }
            return View("Index", indexViewModel);
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