using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.DAL.App.Repositories;
using ee.itcollege.masirg.Contracts.DAL.Base;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.masirg.DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CategoryRepositoryAsync : BaseRepositoryAsync<Category>, ICategoryRepository
    {
        public CategoryRepositoryAsync(IDataContext dataContext) : base(dataContext)
        {
        }

        public async Task<List<DALCategoryDTO>> AllAsync(int organizationId)
        {
            var categories = await RepoDbSet
                .Include(category => category.CategoryName)
                .ThenInclude(name => name.Translations)
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
                .Select(CategoryMapper.FromDomain)
                .ToList();
        }

        public async Task AddAsync(DALCategoryDTO categoryDTO)
        {
            var category = CategoryMapper.FromDAL(categoryDTO);

            await RepoDbSet.AddAsync(category);
        }
    }
}