using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.Identity;

namespace Contracts.DAL.App.Repositories.Identity
{
    public interface IAppUserRepository : IBaseRepositoryAsync<AppUser>
    {
        Task<bool> NicknameExists(string userName);
        Task<bool> EmailExists(string email);
    }
}