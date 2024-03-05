using System;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Services.Models;
using LanguageExchangeHub1.Services.Models.Courses;
using LanguageExchangeHub1.Services.Models.Users;

namespace LanguageExchangeHub1.Services.Contracts
{
	public interface IRequestService
	{
        Task<List<Request>> GetAllAsync();

        Task<Request> GetAsync(string requestId);

        Task<RequestViewModel> CreateAsync(string courseId);
    }
}

