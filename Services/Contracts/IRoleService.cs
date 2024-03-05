using System;
using System.Collections;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Services.Models;
using LanguageExchangeHub1.Services.Models.Courses;

namespace LanguageExchangeHub1.Services.Contracts
{
	public interface IRoleService
	{
        Task<Role> CreateAsync(RoleViewModel courseViewModel);
        Task<IEnumerable<RoleViewModel>> GetAllAsync();
    }
}

