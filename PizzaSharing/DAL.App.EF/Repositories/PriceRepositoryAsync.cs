using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PriceRepositoryAsync : BaseRepositoryAsync<Price>, IPriceRepository
    {
        public PriceRepositoryAsync(IDataContext dataContext) : base(dataContext)
        {
        }

        public override async Task<IEnumerable<Price>> AllAsync()
        {
            return await RepoDbSet
                .Include(price => price.Product)
                .Include(price => price.Change)
                .ToListAsync();
        }

        public override async Task<Price> FindAsync(params object[] id)
        {
            var price = await RepoDbSet.FindAsync(id);

            if (price != null)
            {
                await RepoDbContext.Entry(price).Reference(p => p.Change).LoadAsync();
                await RepoDbContext.Entry(price).Reference(p => p.Product).LoadAsync();
            }

            return price;
        }
    }
}