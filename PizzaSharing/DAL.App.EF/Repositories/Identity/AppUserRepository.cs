using System.Threading.Tasks;
using Contracts.DAL.App.Repositories.Identity;
using ee.itcollege.masirg.Contracts.DAL.Base;
using ee.itcollege.masirg.DAL.Base.EF.Repositories;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories.Identity
{
    public class AppUserRepository : BaseRepositoryAsync<AppUser>, IAppUserRepository
    {
        public AppUserRepository(IDataContext dataContext) : base(dataContext)
        {
        }

        public async Task<bool> NicknameExistsAsync(string userName)
        {
            return await RepoDbSet.AnyAsync(user =>
                userName.ToLower().Equals(user.UserNickname.ToLower()));
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await RepoDbSet.AnyAsync(user =>
                user.Email.ToLower().Equals(email.ToLower()));
        }
    }
}