using System.Collections;

using Biz.Models.Models.Courses;
using Biz.Models.Models.Message;
using Core;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Base;
using Services.Contracts;


namespace LanguageExchangeHub1.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService courseService;
        private readonly ILanguageService languageService;
        private readonly IUserService userService;
        private readonly IUserData userData;
        private readonly IRequestService requestService;
        private readonly IMessageService messageService;

        public CourseController(
            ICourseService courseService, 
            ILanguageService languageService,
            IUserService userService,
            IUserData userData,
            IRequestService requestService,
            IMessageService messageService)
        {
            this.courseService = courseService;
            this.languageService = languageService;
            this.userService = userService;
            this.userData = userData;
            this.requestService = requestService;
            this.messageService = messageService;
        }
        
        public async Task<IActionResult> CreateCourse()
        {          
            var languagaes = await this.languageService.GetAllAsync();
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

            await this.courseService.CreateAsync(courseViewModel);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AddMember()
        {
            return View();
        }

        public async Task<IActionResult> CourseChat(int courseId)        {
            CourseViewModel course = await this.courseService.GetAsync(courseId);
            ViewBag.loggedUser = userService.GetUserByIdAsync(userData.UserId);
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Request(int courseId)
        { 
            await this.requestService.CreateAsync(courseId);          
           
            return RedirectToAction("Index", "Home");
        }

        [HttpPost("/Approve/Course")]
        public async Task<IActionResult> Approve(RequestStatus requestStatus, int requestId,int courseId)
        {
            
            await requestService.SetRequestStatus(requestStatus, requestId);
            return RedirectToAction("CourseChat", "Course",new { courseId });
           

        }
        [HttpPost("/Reject/Course")]
        public async Task<IActionResult> Reject(RequestStatus requestStatus, int requestId, int courseId)
        {
                await requestService.SetRequestStatus(requestStatus, requestId);
                return RedirectToAction("CourseChat", "Course", new { courseId });
        }

        public async Task<IActionResult> CourseAssignments(int courseId)
        {
            CourseViewModel course = await this.courseService.GetAsync(courseId);
            ViewBag.UserId = userData.UserId;
            return View( course);
        }

        public async Task<IActionResult> SendMessage(string userInput,int courseId)
        {
            await messageService.CreateAsync(userInput,courseId);
            return RedirectToAction("CourseChat", "Course", new { courseId });
        }


    }


}