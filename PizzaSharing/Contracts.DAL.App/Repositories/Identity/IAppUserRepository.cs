using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.Identity;

namespace Contracts.DAL.App.Repositories.Identity
{
    public interface IAppUserRepository : IBaseRepositoryAsync<AppUser>
    {
        Task<bool> NicknameExistsAsync(string userName);
        Task<bool> EmailExistsAsync(string email);
    }
}