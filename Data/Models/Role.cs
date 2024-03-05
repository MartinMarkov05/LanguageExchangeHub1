using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace LanguageExchangeHub1.Data.Models
{

	[Table("AspNetRoles")]
	public class Role:IdentityRole 
	{
		
	}
}

