using System;
using System.Collections;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Services.Models.Courses;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LanguageExchangeHub1.Services.Contracts
{
	public interface ILanguageService
	{
        Task<List<Language>> GetAllAsync();

        Task<Language> GetAsync(string languageId);

        Task<Language> CreateAsync(Language language);
    }
}

