using System;
using System.Collections;
using AutoMapper;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Repository;
using LanguageExchangeHub1.Services.Base;
using LanguageExchangeHub1.Services.Contracts;
using LanguageExchangeHub1.Services.Models.Base;
using LanguageExchangeHub1.Services.Models.Courses;
using LanguageExchangeHub1.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LanguageExchangeHub1.Services.Models
{
    public class RoleService : BaseService, IRoleService
    {
        private readonly IEfRepository<Role> _roleRepository;
        private readonly RoleManager<Role> _roleManager;

        public RoleService(IServicesResourceProvider servicesResourceProvider, IUserData userData, RoleManager<Role> roleManager)
            : base(userData,servicesResourceProvider)
        {
            _roleRepository = ServicesResourceProvider.GetEfRepositoryOfType<Role>();
            _roleManager = roleManager;
        }

        public async Task<OperationResponse> CreateAsync(RoleViewModel roleViewModel)
        {
            if (roleViewModel == null)
            {
                throw new ArgumentNullException(nameof(roleViewModel));
            }

            var modelForCreate = roleViewModel.MapRoleViewModelToRoleEntity();
            await _roleManager.CreateAsync(modelForCreate);

            return new OperationResponse { IsSuccessful = true };
        }

        public async Task<IEnumerable<RoleViewModel>> GetAllAsync()
        {
            return await _roleRepository.All()
                .Select(r => r.MapRoleEntityToRoleViewModel())
                .ToListAsync();
        }
    }
}

