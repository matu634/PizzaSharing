using System;
using Contracts.Base;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;

namespace Contracts.BLL.Base.Services
{
    public interface IBaseService : ITrackableInstance
    {
    }
    
    public interface IBaseEntityService<TEntity> : IBaseService, IBaseRepositoryAsync<TEntity> 
        where TEntity : class, IBaseEntity<int>, new()
    {
        
    }
}