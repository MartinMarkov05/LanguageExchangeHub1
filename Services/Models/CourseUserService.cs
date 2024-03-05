using System;
using AutoMapper;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Repository;
using LanguageExchangeHub1.Services.Base;
using LanguageExchangeHub1.Services.Contracts;
using LanguageExchangeHub1.Services.Models.Courses;
using LanguageExchangeHub1.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LanguageExchangeHub1.Services.Models
{
	public class CourseUserService : BaseService, ICourseUserService
	{
        private readonly IEfRepository<CourseUser> _courseUserRepository;
        private readonly IUserService _userService;
        private readonly IUserData _userData;
        private readonly UserManager<User> _userManager;
   

        public CourseUserService(IMapper mapper, IUserData userData,
              IEfRepository<CourseUser> courseRepository, IUserService userService,
              UserManager<User> userManager) : base(mapper, userData)
        {
            _courseUserRepository = courseRepository;
            _userService = userService;
            _userManager = userManager;
            _userData = userData;
           

		}

        public async Task<CourseUser> CreateAsync(Course course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }

            var modelForCreate = new CourseUser
            {
                User = Mapper.Map<User>(await _userService.GetUserByIdAsync(UserData.UserId)),
                UserId = UserData.UserId,
                Course = course,
                CourseId = course.Id
            };

            _courseUserRepository.Add(modelForCreate);
            await _courseUserRepository.SaveChangesAsync();
            return modelForCreate;
        }

        public async Task<CourseUserViewModel> CreateAsync(CourseViewModel courseViewModel)
        {
            var userViewModel = await _userService.GetUserByIdAsync(_userData.UserId);
            
            CourseUserViewModel courseUserView = new CourseUserViewModel
            {

               
                User = userViewModel,
                Course = courseViewModel,
                
            };
            return courseUserView;
        }

        public async Task<List<CourseUserViewModel>> GetAllAsync()
        {
            var allcourseUsers = await _courseUserRepository.All().ToListAsync();
            List<CourseUserViewModel> list = new List<CourseUserViewModel>();
            foreach (var item in allcourseUsers)
            {
               var  courseUserModel = Mapper.Map<CourseUserViewModel>(item);
                list.Add(courseUserModel);
            }

            return list;
        }

        public Task<CourseUserViewModel> GetAsync(string courseUserId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CourseViewModel>> GetAllCoursesByUser(string userId)
        {
            List<CourseViewModel> usersCourses = new List<CourseViewModel>();

            var allcourseUsers = await GetAllAsync();
            foreach (var item in allcourseUsers)
            {
                if (item.User.Id == userId)
                {
                    usersCourses.Add(item.Course);
                }

            }
            return usersCourses;
        }
    }
}

