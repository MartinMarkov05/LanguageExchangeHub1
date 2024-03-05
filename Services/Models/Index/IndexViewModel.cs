using System;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Services.Models.Courses;

namespace LanguageExchangeHub1.Services.Models.Index
{
	public class IndexViewModel
	{
        public List<CourseViewModel> Courses { get; set; }
        public List<CourseViewModel> FilteredCourses { get; set; }
        public List<Language> Languages { get; set; }
        public CourseViewModel CourseViewModel { get; set; }
    }
}

