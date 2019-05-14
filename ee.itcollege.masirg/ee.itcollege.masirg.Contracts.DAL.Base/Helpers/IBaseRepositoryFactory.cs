using System;

namespace ee.itcollege.masirg.Contracts.DAL.Base.Helpers
{
    public interface IBaseRepositoryFactory
    {
        /// <summary>
        /// Get method for repository creation
        /// </summary>
        /// <typeparam name="TRepository">Repository type to post</typeparam>
        /// <returns></returns>
        Func<IDataContext, object> GetRepositoryFactory<TRepository>();
        
        
        /// <summary>
        /// Get method for repo creation based on entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns></returns>
        Func<IDataContext, object> GetRepositoryFactoryForEntity<TEntity>()
            where TEntity: class, IBaseEntity, new();
    }
}