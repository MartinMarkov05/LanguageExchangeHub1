using System;
using System.ComponentModel.DataAnnotations;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Utilities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LanguageExchangeHub1.Services.Models.Users
{
	public class UserRegistrationModel : IMapFrom<User>, IMapTo<User>
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string RepeatPassword { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string? Role { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? RoleList { get; set; }
	}
}

