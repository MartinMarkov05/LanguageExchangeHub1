using System;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Services.Base;
using LanguageExchangeHub1.Services.Contracts;
using LanguageExchangeHub1.Services.Models.Courses;
using LanguageExchangeHub1.Services.Models.Users;
using LanguageExchangeHub1.Utilities;
using LanguageExchangeHub1.Utilities.RequestStatus;

namespace LanguageExchangeHub1.Services.Models
{
	public class RequestViewModel : IMapFrom<Request>, IMapTo<Request>
	{

		public UserViewModelForChat User { get; set; }

		public CourseViewModelForChat Course { get; set; }

		public RequestStatus Status { get; set; }
	}
}

