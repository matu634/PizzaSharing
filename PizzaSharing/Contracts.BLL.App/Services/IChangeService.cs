using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.masirg.Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface IChangeService : IBaseService
    {
        Task<bool> AddChangeAsync(BLLChangeDTO changeDto);
        
        Task<BLLChangeDTO> GetChangeAsync(int changeId);
        
        Task<bool> DeleteChangeAsync(int changeId);
        
        Task<bool> EditChange(BLLChangeDTO changeDto);
    }
}