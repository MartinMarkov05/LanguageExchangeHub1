using System;
using LanguageExchangeHub1.Data;
using LanguageExchangeHub1.Repository;

namespace LanguageExchangeHub1.Services.Base
{
	public class SingleServiceResourceProvider : IServicesResourceProvider
    {
        private readonly ApplicationDbContext dbContext;
        private readonly Dictionary<Type, IEfRepository> repositoriesDictionary;
        private readonly IServiceScope serviceScope;

        public SingleServiceResourceProvider(IServiceScopeFactory serviceScopeFactory, ApplicationDbContext applicationDbContext)
        {
            this.dbContext = applicationDbContext;
            this.serviceScope = serviceScopeFactory.CreateScope();
            this.repositoriesDictionary = new Dictionary<Type, IEfRepository>();
        }

        public IEfRepository<T> GetEfRepositoryOfType<T>() where T : class
        {
            if (!this.repositoriesDictionary.ContainsKey(typeof(T)))
            {
                this.repositoriesDictionary[typeof(T)] = new EfRepository<T>(this.dbContext);
            }

            return this.repositoriesDictionary[typeof(T)] as IEfRepository<T>;
        }

        public void Dispose()
        {
            this.serviceScope.Dispose();
        }
    }
}

