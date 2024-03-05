using System;
using System.ComponentModel.DataAnnotations.Schema;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Services.Models.Users;
using LanguageExchangeHub1.Utilities;

namespace LanguageExchangeHub1.Services.Models.Courses
{
    public class CourseViewModel : IMapFrom<Course>, IMapTo<Course>
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[]? Image { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? ImageUrl { get; set; }

        public List<UserViewModelForChat>? Members { get; set; }
        public List<RequestViewModel>? Requests { get; set; }
        public int LanguageId { get; set; }


	}
}

