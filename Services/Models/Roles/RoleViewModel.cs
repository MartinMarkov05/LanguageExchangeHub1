using System;
using System.ComponentModel.DataAnnotations;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Utilities;

namespace LanguageExchangeHub1.Services.Models
{
	public class RoleViewModel 
	{

		
		[Required]
		public string Name { get; set; }
	}
}

