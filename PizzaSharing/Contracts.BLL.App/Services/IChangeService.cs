using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface IChangeService : IBaseService
    {
        Task<bool> AddChangeAsync(BLLChangeDTO changeDto);
    }
}