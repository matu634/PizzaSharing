using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.masirg.Contracts.BLL.Base.Services;
using ee.itcollege.masirg.Contracts.DAL.Base;
using ee.itcollege.masirg.DAL.Base.EF.Repositories;

namespace ee.itcollege.masirg.BLL.Base.Services
{
    public class BaseEntityService<TEntity, TUnitOfWork> : BaseService<TUnitOfWork>, IBaseEntityService<TEntity> 
        where TEntity : class, IBaseEntity, new()
        where TUnitOfWork : IBaseUnitOfWork
    {
        public BaseEntityService(TUnitOfWork uow) : base(uow)
        {
        }

        public int? GetEntityIdAfterSaveChanges(int oldId)
        {
            throw new NotImplementedException();
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