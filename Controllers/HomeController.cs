using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LanguageExchangeHub1.Models;
using LanguageExchangeHub1.Repository;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Services;
using LanguageExchangeHub1.Services.Models.Courses;
using LanguageExchangeHub1.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using LanguageExchangeHub1.Services.Base;
using LanguageExchangeHub1.Services.Models.Index;
using Newtonsoft.Json;

namespace LanguageExchangeHub1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICourseService _courseService;
    private readonly ILanguageService _languageService;
    private readonly IUserService _userService;
    private readonly IUserData _userData;
    private readonly ICourseUserService _courseUserService;

    public HomeController(ILogger<HomeController> logger, ICourseService courseService,
        ILanguageService languageService, IUserService userService, IUserData userData, ICourseUserService courseUserService)
    {
        _logger = logger;
        _courseService = courseService;
        _languageService = languageService;
        _userService = userService;
        _userData = userData;
        _courseUserService = courseUserService;
    }
    


    [HttpPost]
    public async Task<IActionResult> Search(string courseName, int languageId, string indexViewModelJson)
    {
        
        var indexViewModel = JsonConvert.DeserializeObject<IndexViewModel>(indexViewModelJson);

        indexViewModel.FilteredCourses = await _courseService.GetCoursesByNameAndLangAsync(courseName, languageId);

        return View("Index",indexViewModel);
    }


    [HttpGet]
    public async Task<IActionResult> Index()
    {
       var indexViewModel = TempData["IndexViewModel"] as IndexViewModel;
        if(indexViewModel == null)
        {
         indexViewModel = new IndexViewModel();
        indexViewModel.Courses = await _courseService.GetAllForCurrentUserAsync();
        indexViewModel.Languages  = await _languageService.GetAllAsync();
        TempData["IndexViewModel"] = indexViewModel; 
        }

        return View("Index" ,indexViewModel);
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

    public async Task<IActionResult> Search(string query)
    {
        var courses=  await _courseService.GetAllByNameAsync(query);
        
        return View(courses);
    }

    
}

