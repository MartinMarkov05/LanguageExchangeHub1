using System;
namespace LanguageExchangeHub1.Repository
{
	public class SaveResult
	{
        public bool IsSuccessful { get; set; }

        public string StackTrace { get; set; }

        public string ErrorMessage { get; set; }
    }
}

