using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ProductInCategoryRepositoryAsync : BaseRepositoryAsync<ProductInCategory>, IProductInCategoryRepository
    {
        public ProductInCategoryRepositoryAsync(IDataContext dataContext) : base(dataContext)
        {
        }

        public override async Task<IEnumerable<ProductInCategory>> AllAsync()
        {
            return await RepoDbSet
                .Include(obj => obj.Category)
                    .ThenInclude(category => category.Organization)
                .Include(obj => obj.Product)
                .ToListAsync();
        }

        public override async Task<ProductInCategory> FindAsync(params object[] id)
        {
            var productInCategory = await RepoDbSet.FindAsync(id);

            if (productInCategory != null)
            {
                await RepoDbContext.Entry(productInCategory).Reference(obj => obj.Product).LoadAsync();
                await RepoDbContext.Entry(productInCategory).Reference(obj => obj.Category).LoadAsync();    
            }
            
            return productInCategory;
        }
    }
}