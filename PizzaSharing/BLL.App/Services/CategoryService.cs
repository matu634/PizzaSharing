using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;

namespace BLL.App.Services
{
    public class CategoryService : BaseService<IAppUnitOfWork>, ICategoryService
    {
        public CategoryService(IAppUnitOfWork uow) : base(uow)
        {
        }

        public async Task<bool> AddCategoryAsync(BLLCategoryDTO categoryDTO)
        {
            var organization = await Uow.Organizations.FindWithCategoriesAsync(categoryDTO.OrganizationId);
            if (organization == null) return false;

            await Uow.Categories.AddAsync(new BLLCategoryDTO()
                {OrganizationId = categoryDTO.OrganizationId, CategoryName = categoryDTO.CategoryName});
            await Uow.SaveChangesAsync();
            return true;
        }
    }
}