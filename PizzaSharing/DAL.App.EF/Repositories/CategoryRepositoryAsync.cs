using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.App.DTO;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CategoryRepositoryAsync : BaseRepositoryAsync<Category>, ICategoryRepository
    {
        public CategoryRepositoryAsync(IDataContext dataContext) : base(dataContext)
        {
        }

        public override async Task<IEnumerable<Category>> AllAsync()
        {
            return await RepoDbSet
                .Include(category => category.Organization)
                .ToListAsync();
        }

        public override async Task<Category> FindAsync(params object[] id)
        {
            var category = await base.FindAsync(id);

            if (category != null)
            {
                await RepoDbContext.Entry(category).Reference(cat => cat.Organization).LoadAsync();
            }

            return category;
        }

        public async Task<List<DALCategoryDTO>> AllAsync(int organizationId)
        {
            var categories = await RepoDbSet
                .Include(category => category.ProductsInCategory)
                .ThenInclude(obj => obj.Product)
                .ThenInclude(product => product.ProductName)
                .ThenInclude(name => name.Translations)
                .Include(category => category.ChangesInCategory)
                .ThenInclude(obj => obj.Change)
                .ThenInclude(change => change.ChangeName)
                .ThenInclude(name => name.Translations)
                .Where(category => category.IsDeleted == false && category.OrganizationId == organizationId)
                .ToListAsync();

            return categories
                .Select(category => new DALCategoryDTO()
                {
                    Id = category.Id,
                    Name = category.CategoryName,
                    ChangeNames = category.ChangesInCategory
                        .Where(obj => obj.Change.IsDeleted == false)
                        .Select(obj => obj.Change.ChangeName.Translate())
                        .ToList(),
                    ProductNames = category.ProductsInCategory
                        .Where(obj => obj.Product.IsDeleted == false)
                        .Select(obj => obj.Product.ProductName.Translate())
                        .ToList()
                })
                .ToList();
        }

        public async Task AddAsync(BLLCategoryDTO categoryDTO)
        {
            var category = new Category()
            {
                CategoryName = categoryDTO.CategoryName,
                OrganizationId = categoryDTO.OrganizationId
            };

            await RepoDbSet.AddAsync(category);
        }
    }
}