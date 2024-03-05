using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace LanguageExchangeHub1.Data.Models
{
    [Table("AspNetUsers")]
	public class User: IdentityUser
	{


        

       
		public string? Name { get; set; }
        
        public string? LastName { get; set; }
      
        
        public int? Age { get; set; }

        
        public string? ImageUrl { get; set; }
        
        public int MotherLanguageId { get; set; }
        [InverseProperty(nameof(CourseUser.User))]
        public virtual List<CourseUser> UserCourses { get; set; }

    }
}

