using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.Base;
using DAL.Base.EF.Repositories;

namespace BLL.Base.Services
{
    public class BaseEntityService<TEntity, TUnitOfWork> : BaseService, IBaseEntityService<TEntity> 
        where TEntity : class, IBaseEntity, new()
        where TUnitOfWork : IBaseUnitOfWork
    {
        protected TUnitOfWork Uow;

        public BaseEntityService(TUnitOfWork uow)
        {
            Uow = uow;
        }

        public virtual TEntity Update(TEntity entity)
        {
            return Uow.BaseRepository<TEntity>().Update(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            Uow.BaseRepository<TEntity>().Remove(entity);
        }

        public virtual void Remove(params object[] id)
        {
            Uow.BaseRepository<TEntity>().Remove(id);
        }

        public Task<bool> Exists(int id)
        {
            return Uow.BaseRepository<TEntity>().Exists(id);
        }

        public virtual async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await Uow.BaseRepository<TEntity>().AllAsync();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await Uow.BaseRepository<TEntity>().AddAsync(entity);
        }

        public virtual async Task<TEntity> FindAsync(params object[] id)
        {
            return await Uow.BaseRepository<TEntity>().FindAsync(id);
        }
    }
}