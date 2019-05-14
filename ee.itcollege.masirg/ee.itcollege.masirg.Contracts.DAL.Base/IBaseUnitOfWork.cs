using System.Threading.Tasks;
using ee.itcollege.masirg.Contracts.DAL.Base.Repositories;

namespace ee.itcollege.masirg.Contracts.DAL.Base
{
    public interface IBaseUnitOfWork
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();

        IBaseRepositoryAsync<TEntity> BaseRepository<TEntity>() 
            where TEntity : class, IBaseEntity, new();
    }
}