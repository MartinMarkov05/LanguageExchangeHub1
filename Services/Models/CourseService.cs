using System;
using System.Collections.Generic;
using AutoMapper;

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
   
        
        

        public CourseService(
            IServicesResourceProvider servicesResourceProvider,
               IUserData userData)

  : base( userData, servicesResourceProvider)
        {

            this.courseRepository = ServicesResourceProvider.GetEfRepositoryOfType<Course>();
        }

        public async Task<Course> CreateAsync(CourseViewModel courseViewModel)
        {
            if (courseViewModel == null)
            {
                throw new ArgumentNullException(nameof(courseViewModel));
            }

            Course course = null;
            if (courseViewModel.Id>0)
            {
                course = await courseRepository.All().FirstOrDefaultAsync(c => c.Id == courseViewModel.Id);
            }
            else
            {
                course = new Course();
            }


            courseViewModel.MapCourseViewModelToCourseEntity(ref course, UserData.UserId);

            course.Members = new List<CourseUser>
            {
                new()
                {
                    CourseId = course.Id,
                    UserId = UserData.UserId
                }
            };

           

            courseRepository.Add(course);
            await courseRepository.SaveChangesAsync();
            return course;
        }





        public async Task<CourseViewModel> GetAsync(int courseId)
        {
            var course = await courseRepository.All()
                .Include(c => c.Members)
                .Include(c => c.Requests)
                .Where(c => c.Id == courseId)
                .FirstOrDefaultAsync();

            return course.MapCourseEntityToCourseViewModel();

        }







        public async Task<List<CourseViewModel>> GetCoursesByNameAndLangAsync(string name, int languageId = -1)
        {
            return await courseRepository.All()
                                .Include(c => c.User)
                .Include(c => c.Language)
                 .Include(c => c.Members)
                 .Include(c => c.Requests)
                 .Where(c => (string.IsNullOrWhiteSpace(name) || c.Name == name) && (languageId <= 0 || c.LanguageId == languageId))
                 .Select(c => c.MapCourseEntityToCourseViewModel())
                 .ToListAsync();
        }



        public async Task<List<CourseViewModel>> GetAllForCurrentUserAsync()
        {
            return await courseRepository.All()
                //without include occures npgexception
                .Include(c => c.User)
                .Include(c => c.Language)
                 .Include(c => c.Members)
                 .Include(c =>c.Requests)
                .Where(c => c.Members.Any(m => m.UserId == UserData.UserId))
                .Select(c => c.MapCourseEntityToCourseViewModel())
                .ToListAsync();
        }
    }
}


