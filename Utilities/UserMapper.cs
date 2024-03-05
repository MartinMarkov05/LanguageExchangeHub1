using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Services.Models.Courses;
using LanguageExchangeHub1.Services.Models.Users;

namespace LanguageExchangeHub1.Utilities
{
    public static class UserMapper 
    {
     

        public static  UserViewModelForChat MapUserForChat( this User user)
        {
            UserViewModelForChat userViewModelForChat = new UserViewModelForChat
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
               
            };

            return userViewModelForChat;
        }

        public static async Task<UserViewModel>MapToViewModel(User user)
        {

            var courseModels = new List<CourseViewModelForChat>();
            if(!(user.UserCourses == null))
            {
            foreach (var courseUser in user.UserCourses)
            {
                var courseForChat = await CourseMapper.MapCourseForChat(courseUser.Course);
                
                

               courseModels.Add(courseForChat);
            }
            }


            UserViewModel userViewModel = new UserViewModel
            {
                UserCourses = courseModels,
                Username = user.UserName,
                ImageUrl = user.ImageUrl,
                Id = user.Id,
                Email = user.Email
            };
            return userViewModel;
        }
    }
}