using System;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Services.Models;

namespace LanguageExchangeHub1.Utilities
{
	public static class RequestMapper
	{
        public static RequestViewModel MapRequestEntityToRequestViewModel(this Request request)
        {
            return new RequestViewModel
            {
                Username = request.User?.UserName ?? string.Empty,
                Status = request.RequestStatus
            };
        }
    }
}

