using System.Threading.Tasks;
using ee.itcollege.masirg.Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface IAppUserService : IBaseService
    {
        Task<bool> NicknameExistsAsync(string nickname);

        Task<bool> EmailExistsAsync(string email);
    }
}