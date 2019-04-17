using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.DAL.Base.Repositories
{
    /// <summary>
    /// Repository containing async and common methods, with int as entity primary key
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepositoryAsync<TEntity> : IBaseRepositoryAsync<TEntity, int> 
        where TEntity : class, IBaseEntity<int>, new(){}

    /// <summary>
    /// Repository containing async and common methods
    /// </summary>
    public interface IBaseRepositoryAsync<TEntity, TKey> : IBaseRepositoryCommon<TEntity, TKey>
        where TEntity : class, IBaseEntity<TKey>, new()
        where TKey : struct, IComparable
    {
        Task<IEnumerable<TEntity>> AllAsync();
        
        Task AddAsync(TEntity entity);
        
        Task<TEntity> FindAsync(params object[] id);
    }

    /// <summary>
    /// Common methods all repository types share
    /// </summary>
    public interface IBaseRepositoryCommon<TEntity, TKey>
        where TEntity : class, IBaseEntity<TKey>, new()
        where TKey : struct, IComparable
    {
        TEntity Update(TEntity entity);
        
        void Remove(TEntity entity);
        
        void Remove(params object[] id);

        Task<bool> Exists(int id);
    }
    
    //----------------------------------------------Obsolete repositories-----------------------------------------------
    //TODO: remove compilation error in Obsolete tags
    [Obsolete("Please use IBaseRepositoryAsync<TEntity>", true)]
    public interface IBaseRepository<TEntity> : IBaseRepository<TEntity, int> 
        where TEntity : class, IBaseEntity<int>, new(){}

    [Obsolete("Please use IBaseRepositoryAsync<TEntity, TKey>", true)]
    public interface IBaseRepository<TEntity, TKey> : IBaseRepositoryCommon<TEntity, TKey>
        where TEntity : class, IBaseEntity<TKey>, new()
        where TKey : struct, IComparable
    {
        IEnumerable<TEntity> All();
        
        TEntity Find(params object[] id);
        
        void Add(TEntity entity);
    }
}