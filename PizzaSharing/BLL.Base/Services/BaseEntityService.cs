using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.Base;
using DAL.Base.EF.Repositories;

namespace BLL.Base.Services
{
    public class BaseEntityService<TEntity> : BaseService, IBaseEntityService<TEntity> 
        where TEntity : class, IBaseEntity, new()
    {
        protected IBaseUnitOfWork Uow;

        public BaseEntityService(IBaseUnitOfWork uow)
        {
            Uow = uow;
        }

        public TEntity Update(TEntity entity)
        {
            return Uow.BaseRepository<TEntity>().Update(entity);
        }

        public void Remove(TEntity entity)
        {
            Uow.BaseRepository<TEntity>().Remove(entity);
        }

        public void Remove(params object[] id)
        {
            Uow.BaseRepository<TEntity>().Remove(id);
        }

        public async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await Uow.BaseRepository<TEntity>().AllAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await Uow.BaseRepository<TEntity>().AddAsync(entity);
        }

        public async Task<TEntity> FindAsync(params object[] id)
        {
            return await Uow.BaseRepository<TEntity>().FindAsync(id);
        }
    }
}