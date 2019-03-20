using System;

namespace Contracts.DAL.Base.Helpers
{
    public interface IRepositoryFactory
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