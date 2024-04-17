using System;
using System.IO.Compression;
using Biz.Models.Models;
using Biz.Models.Models.Assignments;
using Biz.Models.Models.Courses;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Base;
using Services.Contracts;

namespace LanguageExchangeHub1.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly IAssignmentService assignmentService;
        private readonly IUserService userService;
        private readonly IUserData userData;

        public AssignmentController(IAssignmentService assignmentService, IUserService userService, IUserData userData)
        {
            this.assignmentService = assignmentService;
            this.userService = userService;
            this.userData = userData;
        }

        public async Task<IActionResult> Assignment(int courseId) {
            var viewModel = new AssignmentViewModel
            {
                CourseId = courseId
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssignment(AssignmentViewModel assignmentViewModel)
        {
            var assignment = await assignmentService.CreateAsync(assignmentViewModel);
            return RedirectToAction("CourseAssignments", "Course", new { assignment.CourseId });
        }

        public async Task<IActionResult> AssignmentPage(int assignmentId)
        {
            AssignmentViewModel assignmentViewModel = await assignmentService.GetAsync(assignmentId);
            if (assignmentViewModel.Materials != null)
            {
                assignmentViewModel.MaterialPreview = assignmentViewModel.Materials.ConvertByteArrayToIFormFile(assignmentViewModel.FileName);
            }
            if (assignmentViewModel.StudentsWork.Count != 0 || assignmentViewModel.StudentsWork != null)
            {
                assignmentViewModel.StudentWorkPreview = assignmentViewModel.ZipStudentWork.ConvertByteArrayToIFormFile("StudentWork.zip");
            }
            ViewBag.loggedUser = userService.GetUserByIdAsync(userData.UserId);
            return View(assignmentViewModel);
        }


        public async Task<IActionResult> DownloadFiles(int assignmentId)
        {
            var assignment = await assignmentService.GetAsync(assignmentId);
            return  File(assignment.Materials, assignment.FileName.GetContentType(), assignment.FileName);
        }

        public async Task<IActionResult> DownloadStudentWorkFiles(int assignmentId)
        {
            var assignment = await assignmentService.GetAsync(assignmentId);
            return File(assignment.ZipStudentWork, "application/zip" , "StudentWork.zip");
        }

        public async Task<IActionResult> AttachWork([FromForm] IFormFile file, int assignmentId) {

            var fileArray = file.ConvertIFormFileToByteArray();
            await assignmentService.AddStudentWork(fileArray, assignmentId, file.FileName);
            return RedirectToAction("AssignmentPage", "Assignment", new { assignmentId });
        }

        public async Task<IActionResult> HandInStudentWork(int assignmentId)
        {
            await assignmentService.HandInStudentWork();
            return RedirectToAction("AssignmentPage", "Assignment", new { assignmentId });
        }

        public async Task<IActionResult> AttachMaterials([FromForm] IFormFile file, int assignmentId)
        {
             var fileArray = file.ConvertIFormFileToByteArray();
            await assignmentService.AddMaterials(fileArray,  assignmentId ,file.FileName );
            return RedirectToAction("AssignmentPage", "Assignment", new {assignmentId});
        }

        [HttpPost]
        public async Task<IActionResult> SaveGrades(AssignmentViewModel assignmentViewModel)
        {
           var assignmentId = await assignmentService.SaveStudentGrades(assignmentViewModel.StudentWorkGrades);
            return RedirectToAction("AssignmentPage", "Assignment", new { assignmentId });
        }
    }
}

