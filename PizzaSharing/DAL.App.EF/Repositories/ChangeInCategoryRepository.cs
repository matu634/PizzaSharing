using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ChangeInCategoryRepository : BaseRepositoryAsync<ChangeInCategory>, IChangeInCategoryRepository
    {
        public ChangeInCategoryRepository(IDataContext dataContext) : base(dataContext)
        {
            
        }

        public async Task AddAsync(int changeId, int categoryId)
        {
            var obj = new ChangeInCategory()
            {
                ChangeId = changeId,
                CategoryId = categoryId
            };

            await RepoDbSet.AddAsync(obj);
        }

        public async Task RemoveByChangeId(int changeId)
        {
            var objects = await RepoDbSet
                .Where(obj => obj.ChangeId == changeId)
                .ToListAsync();
            
            RepoDbSet.RemoveRange(objects);
        }
    }
}