using System;
using Contracts.DAL.Base;

namespace Contracts.BLL.Base.Helpers
{
    public interface IBaseServiceFactory<TUnitOfWork>
        where TUnitOfWork : IBaseUnitOfWork
    {
        Func<TUnitOfWork, object> GetServiceFactory<TService>();
        
        

        Func<TUnitOfWork, object> GetServiceFactoryForEntity<TEntity>()
            where TEntity: class, IBaseEntity, new();
    }
}