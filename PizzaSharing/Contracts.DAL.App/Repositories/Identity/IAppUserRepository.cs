using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;
using ee.itcollege.masirg.Contracts.DAL.Base.Repositories;
using Domain.Identity;

namespace Contracts.DAL.App.Repositories.Identity
{
    public interface IAppUserRepository : IBaseRepositoryAsync<AppUser>
    {
        Task<bool> NicknameExistsAsync(string userName);
        
        Task<bool> EmailExistsAsync(string email);

        new Task<List<DALAppUserDTO>> AllAsync();
    }
}