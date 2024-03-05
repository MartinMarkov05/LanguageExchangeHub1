using System;
using System.Collections;
using AutoMapper;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Repository;
using LanguageExchangeHub1.Services.Base;
using LanguageExchangeHub1.Services.Contracts;
using LanguageExchangeHub1.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LanguageExchangeHub1.Services.Models
{
	public class LanguageService : BaseService, ILanguageService
	{
        private readonly IEfRepository<Language> languageRepository;
        private readonly UserManager<User> userManager;

        public LanguageService(IMapper mapper,

                 IEfRepository<Language> languageRepository
                 ,
                 UserManager<User> userManager,
                 IUserData userData )
            :base(mapper,userData)
		{
            this.userManager = userManager;
            this.languageRepository = languageRepository;
        }

        public Task<Language> CreateAsync(Language language)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Language>> GetAllAsync()
        {
            var languages = await languageRepository.All().ToListAsync();
            return languages; 
        }

        public Task<Language> GetAsync(string languageId)
        {
            throw new NotImplementedException();
        }
    }
}

