using LanguageExchangeHub1.Data;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Services.Contracts;
using LanguageExchangeHub1.Services.Models.Index;

using LanguageExchangeHub1.Services.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LanguageExchangeHub1.Controllers
{
    public class UserController : Controller
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;

        public UserController(ApplicationDbContext applicationDbContext, UserManager<User> userManager, IUserService userService)
        {
            _dbContext = applicationDbContext;

            _userManager = userManager;
            _userService = userService;

        }

        // GET: /<controller>/
        public IActionResult Index(Course course)
        {
        //   if (_userManager.GetUserAsync(User).)
          //  {
            //   return View(course);
            //}
            return View();
        }



        [HttpPost]
        public IActionResult Create(User user)
        {

          //  _dbContext.Users.Add(user);
           // _dbContext.SaveChanges();
            return View();
        }



        public IActionResult Edit()
        {

            return View();
        }
        [Authorize]
        public async Task<IActionResult> Profile()
        {

            var loggedUser = await _userManager.GetUserAsync(User);
            var viewModel = await _userService.GetUserByIdAsync(loggedUser.Id);
            
           
           


            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> EditProfile(UserViewModel viewModel)
        {
            
                var editedUser = await _userService.EditProfile(viewModel);

              
                    return View("Profile",editedUser);
                }
         
    }
}

