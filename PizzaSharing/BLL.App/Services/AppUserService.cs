using System.Threading.Tasks;
using ee.itcollege.masirg.BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;

namespace BLL.App.Services
{
    public class AppUserService : BaseService<IAppUnitOfWork>, IAppUserService
    {
        public AppUserService(IAppUnitOfWork uow) : base(uow)
        {
        }

        public async Task<bool> NicknameExistsAsync(string nickname)
        {
            return await Uow.AppUsers.NicknameExistsAsync(nickname);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await Uow.AppUsers.EmailExistsAsync(email);
        }
    }
}