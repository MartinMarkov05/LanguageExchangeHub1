using System;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Services.Models;
using LanguageExchangeHub1.Services.Models.Courses;
using LanguageExchangeHub1.Services.Models.Users;

namespace LanguageExchangeHub1.Utilities
{
	public static class CourseMapper 
	{


		public static async Task<CourseViewModelForChat> MapCourseForChat(Course course)
		{
			CourseViewModelForChat courseForChat = new CourseViewModelForChat
			{
				Id = course.Id,
				Name = course.Name,
				Description = course.Description
			};

			return courseForChat;
		}

		public static  CourseViewModel MapToViewModel(this Course course, List<UserViewModelForChat> members)
		{




			CourseViewModel courseViewModel = new CourseViewModel
			{
				Members = members,
				Id = course.Id,
				Name = course.Name,
				Image = course.Image,
				Description = course.Description,
				LanguageId = course.LanguageId
				
			};

			return courseViewModel;
		}

		//public static Course MapToModel(CourseViewModel)
	}
}

