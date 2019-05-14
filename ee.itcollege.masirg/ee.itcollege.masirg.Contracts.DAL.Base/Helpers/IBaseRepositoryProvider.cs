using ee.itcollege.masirg.Contracts.DAL.Base.Repositories;

namespace ee.itcollege.masirg.Contracts.DAL.Base.Helpers
{
    public interface IBaseRepositoryProvider
    {
        /// <summary>
        /// Return TRepository from cache or call factory to post it
        /// </summary>
        /// <typeparam name="TRepository"></typeparam>
        /// <returns></returns>
        TRepository GetRepository<TRepository>();

        /// <summary>
        /// Return IBaseRepository from cache or post
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IBaseRepositoryAsync<TEntity> GetRepositoryForEntity<TEntity>() 
            where TEntity : class, IBaseEntity, new();
    }
}