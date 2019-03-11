using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Helpers;
using Contracts.DAL.Base.Repositories;

namespace DAL.Base.EF.Helpers
{
    public class BaseRepositoryProvider : IRepositoryProvider
    {
        //Repo cache
        private readonly Dictionary<Type, object> _repositoryCache = new Dictionary<Type, object>();
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IDataContext _dataContext;

        public BaseRepositoryProvider(IRepositoryFactory repositoryFactory, IDataContext dataContext)
        {
            _repositoryFactory = repositoryFactory;
            _dataContext = dataContext;
        }

        public TRepository GetRepository<TRepository>()
        {
            _repositoryCache.TryGetValue(typeof(TRepository), out var repoObject);
            if (repoObject != null) return (TRepository) repoObject;
            //Repo not found in cache, create it

            var repoCreationMethod = _repositoryFactory.GetRepositoryFactory<TRepository>();
            if (repoCreationMethod == null)
                throw new NullReferenceException("No factory found for repo: " + typeof(TRepository).Name);

            repoObject = repoCreationMethod(_dataContext);
            _repositoryCache[typeof(TRepository)] = repoObject;
            return (TRepository) repoObject;
        }

        public IBaseRepositoryAsync<TEntity> GetRepositoryForEntity<TEntity>() where TEntity : class, IBaseEntity, new()
        {
            _repositoryCache.TryGetValue(typeof(IBaseRepositoryAsync<TEntity>), out var repoObject);
            if (repoObject != null) return (IBaseRepositoryAsync<TEntity>) repoObject;
            //Repo not found in cache, create it

            var repoCreationMethod = _repositoryFactory.GetRepositoryFactoryForEntity<TEntity>();
            if (repoCreationMethod == null)
                throw new NullReferenceException("No factory found for entity: " + typeof(TEntity).Name);

            repoObject = repoCreationMethod(_dataContext);
            _repositoryCache[typeof(IBaseRepositoryAsync<TEntity>)] = repoObject;
            return (IBaseRepositoryAsync<TEntity>) repoObject;
        }
    }
}