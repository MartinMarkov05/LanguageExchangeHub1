using System;
namespace LanguageExchangeHub1.Repository
{
	public interface IEfRepository <T>: IDisposable
	{
        void Add(T entity);

         IQueryable<T> All();

        void Delete(T entity);

        void Delete(params object[] id);

        T GetById(params object[] id);

        void Update(T entity);
        
         Task<int> SaveChangesAsync();

        
    }
}

