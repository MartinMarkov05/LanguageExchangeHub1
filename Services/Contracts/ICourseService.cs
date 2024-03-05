using System;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Services.Models;
using LanguageExchangeHub1.Services.Models.Base;
using LanguageExchangeHub1.Services.Models.Courses;
using LanguageExchangeHub1.Services.Models.Users;

namespace LanguageExchangeHub1.Services
{
	public interface ICourseService
	{
        Task <List<CourseViewModel>> GetAllAsync();

        Task<CourseViewModel> GetAsync(string courseId);

        Task<Course> CreateAsync(CourseViewModel courseViewMode);

        Task<List<CourseViewModel>> GetAllByNameAsync(string name);

        Task<List<CourseViewModel>> GetAllForCurrentUserAsync();

        Task<List<CourseViewModel>> GetAllCoursesByLanguageAsync(int languageId);

        Task<List<CourseViewModel>> GetCoursesByNameAndLangAsync(string name, int languageId);

    }
}

