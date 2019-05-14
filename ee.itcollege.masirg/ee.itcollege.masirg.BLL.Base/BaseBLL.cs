using System;
using System.Threading.Tasks;
using ee.itcollege.masirg.Contracts.BLL.Base;
using ee.itcollege.masirg.Contracts.BLL.Base.Helpers;
using ee.itcollege.masirg.Contracts.BLL.Base.Services;
using ee.itcollege.masirg.Contracts.DAL.Base;

namespace ee.itcollege.masirg.BLL.Base
{
    public class BaseBLL<TUnitOfWork> : IBaseBLL 
        where TUnitOfWork : IBaseUnitOfWork
    {
        protected readonly TUnitOfWork Uow;
        protected readonly IBaseServiceProvider ServiceProvider;

        public BaseBLL(TUnitOfWork uow, IBaseServiceProvider serviceProvider)
        {
            Uow = uow;
            ServiceProvider = serviceProvider;
        }

        public Guid InstanceId { get; } = Guid.NewGuid();

        public async Task<int> SaveChangesAsync()
        {
            return await Uow.SaveChangesAsync();
        }

        public IBaseEntityService<TEntity> BaseEntityService<TEntity>() where TEntity : class, IBaseEntity, new()
        {
            return ServiceProvider.GetEntityService<TEntity>();
        }
    }
}