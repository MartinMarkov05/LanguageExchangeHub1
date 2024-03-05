using System;
using System.ComponentModel.DataAnnotations;

namespace LanguageExchangeHub1.Services.Models.Users
{
	public class UserLoginModel
	{
        [Required(ErrorMessage = "Полето е задължително!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Полето е задължително!")]
        public string Password { get; set; }
    }
}

