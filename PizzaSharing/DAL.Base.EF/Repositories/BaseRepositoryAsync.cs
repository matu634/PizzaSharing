using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class BaseRepositoryAsync<TEntity> : IBaseRepositoryAsync<TEntity> where TEntity : class, IBaseEntity<int>, new()
    {
        protected readonly DbContext RepoDbContext;
        protected readonly DbSet<TEntity> RepoDbSet;
        
        public BaseRepositoryAsync(IDataContext dataContext)
        {
            RepoDbContext =  (dataContext as DbContext) ?? throw new ArgumentNullException(nameof(dataContext));
            RepoDbSet =  RepoDbContext.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await RepoDbSet.ToListAsync();
        }

        public virtual async Task<TEntity> FindAsync(params object[] id)
        {
            return await RepoDbSet.FindAsync(id);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
             await RepoDbSet.AddAsync(entity);
        }

        public virtual TEntity Update(TEntity entity)
        {
            return RepoDbSet.Update(entity).Entity;
        }

        public virtual void Remove(TEntity entity)
        {
            RepoDbSet.Remove(entity);
        }

        public virtual void Remove(params object[] id)
        {
            RepoDbSet.Remove(RepoDbSet.Find(id));
        }

    }
}