using System;
using System.Collections.Generic;
using Contracts.BLL.Base.Helpers;
using Contracts.BLL.Base.Services;
using Contracts.DAL.Base;

namespace BLL.Base.Helpers
{
    public class BaseServiceProvider : IBaseServiceProvider
    {
        protected readonly Dictionary<Type, object> ServiceCache = new Dictionary<Type, object>();
        protected readonly IBaseServiceFactory ServiceFactory;
        protected readonly IBaseUnitOfWork Uow;

        public BaseServiceProvider(IBaseUnitOfWork uow, IBaseServiceFactory serviceFactory)
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
                throw new NullReferenceException("No factory found for repo: " + typeof(TService).Name);

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