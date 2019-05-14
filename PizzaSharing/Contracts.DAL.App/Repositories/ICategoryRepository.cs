using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.masirg.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface ICategoryRepository : IBaseRepositoryAsync<Category>
    {
        Task<List<DALCategoryDTO>> AllAsync(int organizationId);
        
        Task AddAsync(DALCategoryDTO categoryDTO);
    }
}