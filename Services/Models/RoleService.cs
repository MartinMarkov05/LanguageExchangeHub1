using System;
using System.Collections;
using AutoMapper;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Repository;
using LanguageExchangeHub1.Services.Base;
using LanguageExchangeHub1.Services.Contracts;
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

        public RoleService(IEfRepository<Role> roleRepository, IMapper mapper, IUserData userData, RoleManager<Role> roleManager)
            : base(mapper, userData)
        {
            _roleRepository = roleRepository;
            _roleManager = roleManager;
        }

        public async Task<Role> CreateAsync(RoleViewModel roleViewModel)
        {
            if (roleViewModel == null)
            {
                throw new ArgumentNullException(nameof(roleViewModel));
            }
            var modelForCreate = this.Mapper.Map<Role>(roleViewModel);
            await _roleManager.CreateAsync(modelForCreate);
          //  _roleRepository.Add(modelForCreate);
           // await _roleRepository.SaveChangesAsync();
            return modelForCreate;
        }

        public async Task<IEnumerable<RoleViewModel>> GetAllAsync()
        {
            var roles = await _roleRepository.All().ToListAsync();
            List<RoleViewModel> list = new List<RoleViewModel>();
            foreach (var role in roles)
            {
                var result = Mapper.Map<RoleViewModel>(role);
                list.Add(result);
            }
            return list;
        }
    }
}

