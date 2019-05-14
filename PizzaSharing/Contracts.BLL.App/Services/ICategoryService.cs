using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.masirg.Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface ICategoryService : IBaseService
    {
        Task<bool> AddCategoryAsync(BLLCategoryDTO categoryDTO);
    }
}