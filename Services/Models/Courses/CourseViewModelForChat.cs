using System;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Utilities;

namespace LanguageExchangeHub1.Services.Models.Courses
{
	public class CourseViewModelForChat : IMapFrom<Course>, IMapTo<Course>
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

      
	}
}

