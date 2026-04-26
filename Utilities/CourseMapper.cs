using System;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Services.Models;
using LanguageExchangeHub1.Services.Models.Courses;
using LanguageExchangeHub1.Services.Models.Users;

namespace LanguageExchangeHub1.Utilities
{
	public static class CourseMapper 
	{


		public static CourseViewModelForChat MapCourseEntityToCourseForChat(this Course course)
		{
			return new CourseViewModelForChat
			{
				Id = course.Id,
				Name = course.Name,
				Description = course.Description
			};


		}

		public static  CourseViewModel MapCourseEntityToCourseViewModel(this Course course)
		{


            var members = new List<UserViewModelForChat>();

            if (course.Members != null)
            {
                foreach (var member in course.Members)
                {
                    members.Add(member.User.MapUserEntityToUserForChat());
                }
            }

            var requests = new List<RequestViewModel>();

            if (course.Requests != null)
            {
                foreach (var request in course.Requests)
                {
                    requests.Add(request.MapRequestEntityToRequestViewModel());
                }
            }

            return new CourseViewModel
			{
				
				Id = course.Id,
				Name = course.Name,
				Image = course.Image,
				Description = course.Description,
				LanguageId = course.LanguageId,
                Requests = requests,
				Members = members
            };

			
		}

		public static void MapCourseViewModelToCourseEntity(this CourseViewModel courseViewModel, ref Course course, string userId)
		{
			course.Id = courseViewModel.Id;
			course.Name = courseViewModel.Name;
			course.Description = courseViewModel.Description;
			course.LanguageId = courseViewModel.LanguageId;
			course.UserId = userId;
			course.Image = courseViewModel.Image;
		}
	}
}

