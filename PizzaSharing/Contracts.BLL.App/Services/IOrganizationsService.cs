using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface IOrganizationsService : IBaseService
    {
        Task<List<BLLOrganizationDTO>> GetOrganizationsMinDTOAsync();
        
        Task<BLLOrganizationAllDTO> GetOrganizationAllDTOAsync(int id);
        
        Task<BLLOrganizationWithCategoriesDTO> GetOrganizationWithCategoriesAsync(int organizationId);
    }
}