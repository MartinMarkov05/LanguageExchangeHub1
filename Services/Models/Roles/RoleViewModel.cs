using System;
using System.ComponentModel.DataAnnotations;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Utilities;

namespace LanguageExchangeHub1.Services.Models
{
	public class RoleViewModel : IMapFrom<Role>, IMapTo<Role>
	{

		public string Id { get; set; }
		[Required]
		public string Name { get; set; }
	}
}

