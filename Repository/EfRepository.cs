using System;
using LanguageExchangeHub1.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LanguageExchangeHub1.Repository
{
	public class EfRepository<T> : IdentityDbContext, IEfRepository<T> where T : class
    {
        private readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1);
        protected ApplicationDbContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }

        public EfRepository(ApplicationDbContext applicationDbContext)
		{
            DbContext = applicationDbContext;
            DbSet = applicationDbContext.Set<T>();

		}
        

        public void Add(T entity)
        {
            var entry = this.DbContext.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.DbSet.Add(entity);
            }

        }

        public IQueryable<T> All()
        {
            return this.DbSet.AsQueryable();
        }

        public void Delete(T entity)
        {
            var entry = this.DbContext.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(entity);
                this.DbSet.Remove(entity);
            }
        }

        public void Delete(params object[] id)
        {
            var entity = this.GetById(id);
            if (entity != null)
            {
                this.Delete(entity);
            }
        }

        public void Dispose()
        {
            {
                if (this.DbContext != null)
                {
                    this.DbContext.Dispose();
                }
            }
        }

        public T GetById(params object[] id)
        {
            return this.DbSet.Find(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            await this.semaphoreSlim.WaitAsync();
            try
            {
              var result= await this.DbContext.SaveChangesAsync();
                return result;
            }
            catch (Exception e)
            {
                
                throw e;

            }
        }

        public void Update(T entity)
        {
            var entry = this.DbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        
    }
}

