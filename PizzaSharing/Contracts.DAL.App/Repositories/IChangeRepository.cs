using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IChangeRepository : IBaseRepositoryAsync<Change>
    {
        Task<List<DALChangeDTO>> AllAsync(int organizationId);
        
        Task<DALChangeDTO> AddAsync(DALChangeDTO changeDTO);
        
        Task<DALChangeDTO> FindDTOAsync(int changeId);
        
        Task<bool> RemoveSoft(int changeId);
        
        Task<DALChangeDTO> EditAsync(DALChangeDTO changeDTO);
    }
}