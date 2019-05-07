using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using DAL.App.DTO;

namespace BLL.App.Services
{
    public class CategoryService : BaseService<IAppUnitOfWork>, ICategoryService
    {
        public CategoryService(IAppUnitOfWork uow) : base(uow)
        {
        }

        public async Task<bool> AddCategoryAsync(BLLCategoryDTO categoryDTO)
        {
            var organization = await Uow.Organizations.Exists(categoryDTO.OrganizationId);
            if (!organization) return false;

            await Uow.Categories.AddAsync(new DALCategoryDTO()
            {
                Name = categoryDTO.CategoryName, 
                OrganizationId = categoryDTO.OrganizationId
            });
            await Uow.SaveChangesAsync();
            return true;
        }
    }
}