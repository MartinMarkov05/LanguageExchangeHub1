using System;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Services.Models.Courses;
using LanguageExchangeHub1.Utilities;

namespace LanguageExchangeHub1.Services.Models.Users
{
	public class UserViewModel : IMapFrom<User>, IMapTo<User>
    {
		
        public string Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? Birthday { get; set; }

        public string ImageUrl { get; set; }

        public List<CourseViewModelForChat> UserCourses { get; set; }

    }
}

