using System;
using System.Collections.Generic;
using BLL.Base.Services;
using Contracts.BLL.Base.Helpers;
using Contracts.DAL.Base;

namespace BLL.Base.Helpers
{
    public class BaseServiceFactory : IBaseServiceFactory
    {
        protected readonly Dictionary<Type, Func<IBaseUnitOfWork, object>> ServiceFactoryMethods;

        //-------------------------------------------------Constructors-------------------------------------------------
        public BaseServiceFactory()
        {
            ServiceFactoryMethods = new Dictionary<Type, Func<IBaseUnitOfWork, object>>();
        }
        
        public BaseServiceFactory(Dictionary<Type, Func<IBaseUnitOfWork, object>> serviceFactoryMethods)
        {
            ServiceFactoryMethods = serviceFactoryMethods;
        }

        //------------------------------------------Repository factory methods------------------------------------------
        public Func<IBaseUnitOfWork, object> GetServiceFactory<TService>()
        {
            ServiceFactoryMethods.TryGetValue(typeof(TService), out var serviceCreationMethod);
            return serviceCreationMethod;
        }

        public Func<IBaseUnitOfWork, object> GetServiceFactoryForEntity<TEntity>() where TEntity : class, IBaseEntity, new()
        {
            return (uow) => new BaseEntityService<TEntity>(uow);
        }
    }
}