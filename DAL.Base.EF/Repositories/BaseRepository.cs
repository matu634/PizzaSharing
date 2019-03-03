using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        protected readonly DbContext RepoDbContext;
        protected readonly DbSet<TEntity> RepoDbSet;
        
        public BaseRepository(DbContext dbContext)
        {
            RepoDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            RepoDbSet = dbContext.Set<TEntity>();
        }
        
        public virtual IEnumerable<TEntity> All()
        {
            return RepoDbSet.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await RepoDbSet.ToListAsync();
        }

        public virtual TEntity Find(params object[] id)
        {
            return RepoDbSet.Find(id);
        }

        public virtual async Task<TEntity> FindAsync(params object[] id)
        {
            return await RepoDbSet.FindAsync(id);
        }

        public virtual void Add(TEntity entity)
        {
            RepoDbSet.Add(entity);
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