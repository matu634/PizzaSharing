using System;
using System.Collections.Generic;
using BLL.Base.Services;
using Contracts.BLL.Base.Helpers;
using Contracts.DAL.Base;

namespace BLL.Base.Helpers
{
    public class BaseServiceFactory<TUnitOfWork> : IBaseServiceFactory<TUnitOfWork> where TUnitOfWork : IBaseUnitOfWork
    {
        protected readonly Dictionary<Type, Func<TUnitOfWork, object>> ServiceFactoryMethods;

        //-------------------------------------------------Constructors-------------------------------------------------
        public BaseServiceFactory()
        {
            ServiceFactoryMethods = new Dictionary<Type, Func<TUnitOfWork, object>>();
        }
        
        public BaseServiceFactory(Dictionary<Type, Func<TUnitOfWork, object>> serviceFactoryMethods)
        {
            ServiceFactoryMethods = serviceFactoryMethods;
        }

        //------------------------------------------Repository factory methods------------------------------------------
        public Func<TUnitOfWork, object> GetServiceFactory<TService>()
        {
            ServiceFactoryMethods.TryGetValue(typeof(TService), out var serviceCreationMethod);
            return serviceCreationMethod;
        }

        public Func<TUnitOfWork, object> GetServiceFactoryForEntity<TEntity>() where TEntity : class, IBaseEntity, new()
        {
            return (uow) => new BaseEntityService<TEntity, TUnitOfWork>(uow);
        }

        protected virtual void Add<TService>(Func<TUnitOfWork, TService> creationMethod)
            where TService : class
        {
            ServiceFactoryMethods.Add(typeof(TService), creationMethod);
        }



    }
}