using LanguageExchangeHub1.Services.Base;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LanguageExchangeHub1.Services.Base
{
    public class UserData : IUserData
    {
        public UserData(IHttpContextAccessor httpContextAccessor)
        {
            this.UserId = httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public string UserId { get; set; }
    }
}

