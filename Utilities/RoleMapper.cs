using System;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Services.Models;

namespace LanguageExchangeHub1.Utilities
{
	public static class RoleMapper
	{
        public static Role MapRoleViewModelToRoleEntity(this RoleViewModel model)
        {
            return new Role
            {
              
                Name = model.Name
            };
        }

        public static RoleViewModel MapRoleEntityToRoleViewModel(this Role role)
        {
            return new RoleViewModel
            {
               
                Name = role.Name
            };
        }
    }
}

