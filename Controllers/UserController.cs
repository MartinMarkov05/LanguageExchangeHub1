using Biz.Models.Models.Users;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Base;
using Services.Contracts;

namespace LanguageExchangeHub1.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService userService;
        private readonly IUserData userData;

        public UserController( IUserService userService, IUserData userData)
        {
           this.userService = userService;
           this.userData = userData;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            
            var viewModel = await userService.GetUserByIdAsync(userData.UserId);
            if(viewModel.Image != null)
            {
viewModel.ImagePreview =  viewModel.Image.ConvertByteArrayToIFormFile(viewModel.FileName);
            }
           
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserViewModel viewModel)
        {
            viewModel.Image = viewModel.ImagePreview.ConvertIFormFileToByteArray();
            viewModel.FileName = viewModel.ImagePreview.FileName;
            await userService.EditProfile(viewModel);
            return RedirectToAction("Profile");
        }


        public async Task<IActionResult> UserAssignements()
        {
            return View("UserAssignements");
        }
    }
}
