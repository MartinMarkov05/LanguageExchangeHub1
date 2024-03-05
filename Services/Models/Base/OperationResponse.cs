using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Repository;
using LanguageExchangeHub1.Utilities;

namespace LanguageExchangeHub1.Services.Models.Base
{
    public class OperationResponse : IMapFrom<SaveResult>
    {
        public bool IsSuccessful { get; set; }

        public string ErrorMessage { get; set; }
    }
}

