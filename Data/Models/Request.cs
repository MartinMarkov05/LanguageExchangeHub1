using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LanguageExchangeHub1.Utilities.RequestStatus;

namespace LanguageExchangeHub1.Data.Models
{

    [Table("Request")]
    public class Request
	{
		[Key]
		public int  Id { get; set; }
		[ForeignKey(nameof(Course))]
		public string CourseId { get; set; }
		public virtual Course Course { get; set; }
		[ForeignKey(nameof(User))]
        public string UserId { get; set; }
		public virtual User User { get; set; }
		public RequestStatus RequestStatus { get; set; }

    }
}

