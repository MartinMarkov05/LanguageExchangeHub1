using System;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Services.Models;
using LanguageExchangeHub1.Services.Models.Courses;

namespace LanguageExchangeHub1.Services.Contracts
{
	public interface ICourseUserService
	{

        Task<List<CourseUserViewModel>> GetAllAsync();

        Task<CourseUserViewModel> GetAsync(string courseUserId);

        Task<CourseUser> CreateAsync(Course course);

        Task<CourseUserViewModel> CreateAsync(CourseViewModel courseViewModel);
        Task<List<CourseViewModel>> GetAllCoursesByUser(string userId);
    }
}

