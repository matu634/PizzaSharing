using System;
using System.Threading.Tasks;
using ee.itcollege.masirg.Contracts.Base;
using ee.itcollege.masirg.Contracts.BLL.Base.Services;
using ee.itcollege.masirg.Contracts.DAL.Base;

namespace ee.itcollege.masirg.Contracts.BLL.Base
{
    public interface IBaseBLL : ITrackableInstance
    {
        IBaseEntityService<TEntity> BaseEntityService<TEntity>() where TEntity : class, IBaseEntity, new();

        Task<int> SaveChangesAsync();
    }
}