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
        

        public LanguageService(

                 IServicesResourceProvider servicesResourceProvider
                 ,
               
                 IUserData userData )
            :base(userData, servicesResourceProvider)
		{
          
            this.languageRepository = ServicesResourceProvider.GetEfRepositoryOfType<Language>();
        }


        public async Task<List<Language>> GetAllAsync()
        {
            return await this.languageRepository.All().ToListAsync();
        }

    }
}

