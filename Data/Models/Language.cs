namespace LanguageExchangeHub1.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Language")]
    public class Language
    {

       [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
    }


}