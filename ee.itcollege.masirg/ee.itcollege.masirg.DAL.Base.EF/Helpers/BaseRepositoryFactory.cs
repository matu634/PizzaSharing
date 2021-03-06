using System;
using System.Collections.Generic;
using ee.itcollege.masirg.Contracts.DAL.Base;
using ee.itcollege.masirg.Contracts.DAL.Base.Helpers;
using ee.itcollege.masirg.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ee.itcollege.masirg.DAL.Base.EF.Helpers
{
    public class BaseRepositoryFactory : IBaseRepositoryFactory
    {
        public readonly Dictionary<Type, Func<IDataContext, object>> RepoFactoryMethods;

        //-------------------------------------------------Constructors-------------------------------------------------
        public BaseRepositoryFactory()
        {
            RepoFactoryMethods = new Dictionary<Type, Func<IDataContext, object>>();
        }

        public BaseRepositoryFactory(Dictionary<Type, Func<IDataContext, object>> repoFactoryMethods)
        {
            RepoFactoryMethods = repoFactoryMethods;
        }

        //------------------------------------------Repository factory methods------------------------------------------
        public Func<IDataContext, object> GetRepositoryFactory<TRepository>()
        {
            RepoFactoryMethods.TryGetValue(typeof(TRepository), out var repoCreationMethod);
            return repoCreationMethod;
        }

        public Func<IDataContext, object> GetRepositoryFactoryForEntity<TEntity>()
            where TEntity : class, IBaseEntity, new()
        {
            return (context) => new BaseRepositoryAsync<TEntity>(context);
        }
        
        public void Add<TRepository>(Func<IDataContext, TRepository> creationMethod)
            where TRepository : class
        {
            RepoFactoryMethods.Add(typeof(TRepository), creationMethod);
        }

    }
}