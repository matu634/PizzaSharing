using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.masirg.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IChangeInCategoryRepository : IBaseRepositoryAsync<ChangeInCategory>
    {
        Task AddAsync(int changeId, int categoryId);
        
        Task RemoveByChangeId(int changeId);
        
        Task<List<DALChangeDTO>> GetChangesByCategoryIdsAsync(int[] categoryIds);
    }
}