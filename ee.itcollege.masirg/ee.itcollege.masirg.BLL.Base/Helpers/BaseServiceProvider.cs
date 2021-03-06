using System;
using System.Collections.Generic;
using ee.itcollege.masirg.Contracts.BLL.Base.Helpers;
using ee.itcollege.masirg.Contracts.BLL.Base.Services;
using ee.itcollege.masirg.Contracts.DAL.Base;

namespace ee.itcollege.masirg.BLL.Base.Helpers
{
    public class BaseServiceProvider<TUnitOfWork> : IBaseServiceProvider 
        where TUnitOfWork : IBaseUnitOfWork
    {
        protected readonly Dictionary<Type, object> ServiceCache = new Dictionary<Type, object>();
        
        protected readonly IBaseServiceFactory<TUnitOfWork> ServiceFactory;
        protected readonly TUnitOfWork Uow;

        public BaseServiceProvider(TUnitOfWork uow, IBaseServiceFactory<TUnitOfWork> serviceFactory)
        {
            Uow = uow;
            ServiceFactory = serviceFactory;
        }


        public TService GetService<TService>()
        {
            ServiceCache.TryGetValue(typeof(TService), out var serviceObject);
            if (serviceObject != null) return (TService) serviceObject;
            //Repo not found in cache, post it

            var serviceCreationMethod = ServiceFactory.GetServiceFactory<TService>();
            if (serviceCreationMethod == null)
                throw new NullReferenceException("No factory found for service: " + typeof(TService).Name);

            serviceObject = serviceCreationMethod(Uow);
            ServiceCache[typeof(TService)] = serviceObject;
            return (TService) serviceObject;
        }

        public IBaseEntityService<TEntity> GetEntityService<TEntity>() where TEntity : class, IBaseEntity, new()
        {
            ServiceCache.TryGetValue(typeof(IBaseEntityService<TEntity>), out var serviceObject);
            if (serviceObject != null) return (IBaseEntityService<TEntity>) serviceObject;
            //Repo not found in cache, post it

            var repoCreationMethod = ServiceFactory.GetServiceFactoryForEntity<TEntity>();
            if (repoCreationMethod == null)
                throw new NullReferenceException("No factory found for entity: " + typeof(TEntity).Name);

            serviceObject = repoCreationMethod(Uow);
            ServiceCache[typeof(IBaseEntityService<TEntity>)] = serviceObject;
            return (IBaseEntityService<TEntity>) serviceObject;
        }
    }
}