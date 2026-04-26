using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Services.Models.Courses;
using LanguageExchangeHub1.Services.Models.Users;

namespace LanguageExchangeHub1.Utilities
{
    public static class UserMapper 
    {
     

        public static  UserViewModelForChat MapUserEntityToUserForChat( this User user)
        {
            return new UserViewModelForChat
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email
               
            };
        }

        public static UserViewModel MapUserEntityToUserViewModel(this User user)
        {

            var courseModels = new List<CourseViewModelForChat>();
            if(!(user.UserCourses == null))
            {
            foreach (var courseUser in user.UserCourses)
            {
                    courseModels.Add(courseUser.Course.MapCourseEntityToCourseForChat());
            }
            }


            return new UserViewModel
            {
                UserCourses = courseModels,
                Username = user.UserName,
                ImageUrl = user.ImageUrl,
                Id = user.Id,
                Email = user.Email
            };
         
        }


        public static User MapUserRegistrationModelToUserEntity(this UserRegistrationModel model) {

            return new User

            {
            
                UserName = model.Username,
                
                Email = model.Email
               
            };
        }

    }
}