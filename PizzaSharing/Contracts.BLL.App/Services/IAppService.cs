using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using PublicApi.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IAppService: IBaseService
    {
        Task<UserDashboardDTO> GetUserDashboard(int userId);
        
        List<OrganizationDTO> GetOrganizationsCategoriesAndProducts();

        List<ChangeDTO> GetValidProductsChanges();

        List<AppUserDTO> GetValidParticipants();
    }
}