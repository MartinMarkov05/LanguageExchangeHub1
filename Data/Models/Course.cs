using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageExchangeHub1.Data.Models
{
    [Table("Course")]
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Image")]
        public byte[]? Image { get; set; }
        [InverseProperty(nameof(CourseUser.Course))]
        public virtual List<CourseUser> Members { get; set; }
        
        [ForeignKey(nameof(Language))]
		public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
        
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [InverseProperty(nameof(Request.Course))]
        public virtual List<Request> Requests { get; set; }

        public int? Test { get; set; }


    }
}

