using System;
using System.Threading.Tasks;
using Contracts.BLL.Base;
using Contracts.BLL.Base.Helpers;
using Contracts.BLL.Base.Services;
using Contracts.DAL.Base;

namespace BLL.Base
{
    public class BaseBLL : IBaseBLL
    {
        protected readonly IBaseUnitOfWork Uow;
        protected readonly IBaseServiceProvider ServiceProvider;

        public BaseBLL(IBaseUnitOfWork uow, IBaseServiceProvider serviceProvider)
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