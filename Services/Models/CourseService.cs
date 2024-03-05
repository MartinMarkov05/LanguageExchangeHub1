using System;
using System.Collections.Generic;
using AutoMapper;
using Azure;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Repository;
using LanguageExchangeHub1.Services.Base;
using LanguageExchangeHub1.Services.Contracts;
using LanguageExchangeHub1.Services.Models;
using LanguageExchangeHub1.Services.Models.Base;
using LanguageExchangeHub1.Services.Models.Courses;
using LanguageExchangeHub1.Services.Models.Users;
using LanguageExchangeHub1.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LanguageExchangeHub1.Services
{
    public class CourseService : BaseService, ICourseService
    {


        private readonly IEfRepository<Course> courseRepository;
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;
        private readonly ICourseUserService courseUserService;
        private readonly IEfRepository<User> userReEposiyory;
        private readonly IEfRepository<Request> requestRepository;

        public CourseService(IMapper mapper,

                 IEfRepository<Course> courseRepository
                 ,

                 IUserData userData, IUserService _userService,
                 UserManager<User> _userManager, ICourseUserService _courseUserService,
                 IEfRepository<User> _userRepository,
                 IEfRepository<Request> _requestRepository)

  : base(mapper, userData)
        {

            this.userService = _userService;
            this.courseRepository = courseRepository;
            this.userManager = _userManager;
            courseUserService = _courseUserService;
            userReEposiyory = _userRepository;
            requestRepository = _requestRepository;
        }

        public async Task<Course> CreateAsync(CourseViewModel courseViewModel)
        {
            if (courseViewModel == null)
            {
                throw new ArgumentNullException(nameof(courseViewModel));
            }

            var course = this.Mapper.Map<Course>(courseViewModel);
            course.UserId = UserData.UserId;
            course.Members = new List<CourseUser>
            {
                new()
                {
                   CourseId = course.Id,
                UserId =UserData.UserId
                }
               
            };

            course.Requests = new List<Request>();

            courseRepository.Add(course);
            await courseRepository.SaveChangesAsync();
            return course;
        }



        public async Task<List<CourseViewModel>> GetAllAsync()
        {
            var courseList = await courseRepository.All().ToListAsync();
            return Mapper.Map<List<CourseViewModel>>(courseList);
        }

        public async Task<List<CourseViewModel>> GetAllCoursesByLanguageAsync(int languageId)
        {
            var courseList = await courseRepository.All()
                .Where(c => c.LanguageId == languageId)
                .ToListAsync();
            return Mapper.Map<List<CourseViewModel>>(courseList);
        }

        public async Task<CourseViewModel> GetAsync(string courseId)
        {
            var course = await courseRepository.All()
                .Include(c => c.Members)
                
                .Where(c => c.Id == courseId)
                
                .FirstOrDefaultAsync();
            //how to map courseuser to userviewmodel
         var userIds = course.Members.Select(c => c.UserId);
          var members =  await  userReEposiyory.All().Where(u => userIds.Any(id => id == u.Id)).Select(u => u.MapUserForChat()).ToListAsync();
          var requests = await requestRepository.All().Where(r => r.CourseId == course.Id).ToListAsync();

   //         List<RequestViewModel> requests = new List<RequestViewModel>();
     //       foreach (var item in course.Requests)
       //     {
         //      var request = Mapper.Map<RequestViewModel>(item);
           //     requests.Add(request);

            //}
            var courseViewModel =  course.MapToViewModel(members);
            return courseViewModel;

        }







        public async Task<List<CourseViewModel>> GetCoursesByNameAndLangAsync(string name, int languageId)
        {
            var courseList = await courseRepository.All()
                .Where(c => (string.IsNullOrWhiteSpace(name) || c.Name == name) && (languageId <=0 || c.LanguageId == languageId))
                .ToListAsync();
            return  Mapper.Map<List<CourseViewModel>>(courseList);
        }

        public async Task<List<CourseViewModel>> GetAllByNameAsync(string name)
        {
            var courseList = await courseRepository.All()
                .Where(c => c.Name == name )
                .ToListAsync();
            return  Mapper.Map<List<CourseViewModel>>(courseList);
        }

        public async Task<List<CourseViewModel>> GetAllForCurrentUserAsync()
        {
            var courseList = await courseRepository.All()
                .Where(c => c.Members.Any(m=>m.UserId == UserData.UserId))
                .ToListAsync();
            return  Mapper.Map<List<CourseViewModel>>(courseList);
        }
    }
}


