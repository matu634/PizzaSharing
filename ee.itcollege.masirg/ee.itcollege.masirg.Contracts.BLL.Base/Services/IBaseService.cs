using System;
using ee.itcollege.masirg.Contracts.Base;
using ee.itcollege.masirg.Contracts.DAL.Base;
using ee.itcollege.masirg.Contracts.DAL.Base.Repositories;

namespace ee.itcollege.masirg.Contracts.BLL.Base.Services
{
    public interface IBaseService : ITrackableInstance
    {
    }
    
    public interface IBaseEntityService<TEntity> : IBaseService, IBaseRepositoryAsync<TEntity> 
        where TEntity : class, IBaseEntity<int>, new()
    {
        
    }
}