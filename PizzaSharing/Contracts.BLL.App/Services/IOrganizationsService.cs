using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.masirg.Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface IOrganizationsService : IBaseService
    {
        Task<List<BLLOrganizationDTO>> GetOrganizationsMinDTOAsync();
        
        Task<BLLOrganizationDTO> GetOrganizationAllDTOAsync(int id);
        
        Task<BLLOrganizationDTO> GetOrganizationWithCategoriesAsync(int organizationId);
        
        Task<BLLOrganizationDTO> GetOrganizationMinAsync(int organizationId);
        
        Task<bool> AddOrganizationAsync(BLLOrganizationDTO organization);
    }
}