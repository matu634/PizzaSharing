using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IChangeInCategoryRepository : IBaseRepositoryAsync<ChangeInCategory>
    {
        Task AddAsync(int changeId, int categoryId);
        
        Task RemoveByChangeId(int changeId);
    }
}