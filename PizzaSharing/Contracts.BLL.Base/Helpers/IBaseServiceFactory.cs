using System;
using Contracts.DAL.Base;

namespace Contracts.BLL.Base.Helpers
{
    public interface IBaseServiceFactory
    {
        Func<IBaseUnitOfWork, object> GetServiceFactory<TService>();
        
        

        Func<IBaseUnitOfWork, object> GetServiceFactoryForEntity<TEntity>()
            where TEntity: class, IBaseEntity, new();
    }
}