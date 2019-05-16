using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.masirg.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IChangeRepository : IBaseRepositoryAsync<Change>
    {
        Task<List<DALChangeDTO>> AllAsync(int organizationId);
        
        Task<DALChangeDTO> AddAsync(DALChangeDTO changeDTO);
        
        /// <summary>
        /// Find change with name and categories
        /// </summary>
        /// <param name="changeId"></param>
        /// <returns></returns>
        Task<DALChangeDTO> FindDTOAsync(int changeId);
        
        Task<bool> RemoveSoft(int changeId);
        
        Task<DALChangeDTO> EditAsync(DALChangeDTO changeDTO);
    }
}