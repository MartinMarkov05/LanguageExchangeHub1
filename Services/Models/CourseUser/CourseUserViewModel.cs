using System;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Services.Models.Courses;
using LanguageExchangeHub1.Services.Models.Users;
using LanguageExchangeHub1.Utilities;

namespace LanguageExchangeHub1.Services.Models
{
	public class CourseUserViewModel 
	{
        public int Id { get; set; }
        public CourseViewModel Course { get; set; }

        
        public UserViewModel User { get; set; }
    }
}

