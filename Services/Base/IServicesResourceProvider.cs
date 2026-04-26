using System;
using LanguageExchangeHub1.Repository;

namespace LanguageExchangeHub1.Services.Base
{
	public interface IServicesResourceProvider
	{
        IEfRepository<T> GetEfRepositoryOfType<T>() where T : class;
    }
}

