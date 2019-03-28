using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ProductRepositoryAsync : BaseRepositoryAsync<Product>, IProductRepository
    {
        public ProductRepositoryAsync(IDataContext dataContext) : base(dataContext)
        {
        }

        public override async Task<IEnumerable<Product>> AllAsync()
        {
            return await RepoDbSet
                .Include(obj => obj.Organization)
                .ToListAsync();
        }

        public override async Task<Product> FindAsync(params object[] id)
        {
            var productInCategory = await RepoDbSet.FindAsync(id);

            if (productInCategory != null)
            {
                await RepoDbContext.Entry(productInCategory).Reference(obj => obj.Organization).LoadAsync();
            }
            
            return productInCategory;
        }
    }
}