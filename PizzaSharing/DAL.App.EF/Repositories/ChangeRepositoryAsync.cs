using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ChangeRepositoryAsync : BaseRepositoryAsync<Change> , IChangeRepository
    {
        public ChangeRepositoryAsync(IDataContext dataContext) : base(dataContext)
        {
            
        }

        public override async Task<IEnumerable<Change>> AllAsync()
        {
            return await RepoDbSet
                .Include(change => change.Category)
                .Include(change => change.Organization)
                .ToListAsync();
        }


        public override async Task<Change> FindAsync(params object[] id)
        {
            var change = await RepoDbSet.FindAsync(id);
            if (change != null)
            {
                await RepoDbContext.Entry(change).Reference(c => c.Category).LoadAsync();
                await RepoDbContext.Entry(change).Reference(c => c.Organization).LoadAsync();
            }

            return change;
        }
    }
}