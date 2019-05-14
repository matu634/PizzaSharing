using System;
using ee.itcollege.masirg.Contracts.DAL.Base;

namespace ee.itcollege.masirg.Contracts.BLL.Base.Helpers
{
    public interface IBaseServiceFactory<TUnitOfWork>
        where TUnitOfWork : IBaseUnitOfWork
    {
        Func<TUnitOfWork, object> GetServiceFactory<TService>();
        
        

        Func<TUnitOfWork, object> GetServiceFactoryForEntity<TEntity>()
            where TEntity: class, IBaseEntity, new();
    }
}