using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;
using PublicApi.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IAppService: IBaseService
    {
        Task<BLLUserDashboardDTO> GetUserDashboard(int userId);
        
        Task<List<BLLOrganizationDTO>> GetOrganizationsCategoriesAndProductsAsync(int receiptId);

        List<ChangeDTO> GetValidProductsChanges();

        List<AppUserDTO> GetValidParticipants();
    }
}