using ee.itcollege.masirg.Contracts.BLL.Base.Services;
using ee.itcollege.masirg.Contracts.DAL.Base;
using ee.itcollege.masirg.Contracts.DAL.Base.Repositories;

namespace ee.itcollege.masirg.Contracts.BLL.Base.Helpers
{
    public interface IBaseServiceProvider
    {
        TService GetService<TService>();

        IBaseEntityService<TEntity> GetEntityService<TEntity>() 
            where TEntity : class, IBaseEntity, new();
        
    }
}