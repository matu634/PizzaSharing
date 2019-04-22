using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;

namespace BLL.App.Services
{
    public class OrganizationsService : BaseService<IAppUnitOfWork>, IOrganizationsService
    {
        public OrganizationsService(IAppUnitOfWork uow) : base(uow)
        {
        }

        public async Task<List<BLLOrganizationDTO>> GetOrganizationsMinDTOAsync()
        {
            var dtos = await Uow.Organizations.AllDtoMinAsync();

            return dtos
                .Select(dto => new BLLOrganizationDTO(dto.Id, dto.Name))
                .ToList();
        }

        public async Task<BLLOrganizationAllDTO> GetOrganizationAllDTOAsync(int id)
        {
            var organization = await Uow.Organizations.FindMinDTOAsync(id);
            if (organization == null) return null;

            var categories = await Uow.Categories.AllAsync(organizationId: id);
            var products = await Uow.Products.AllAsync(organizationId: id);
            var changes = await Uow.Changes.AllAsync(organizationId: id);

            return new BLLOrganizationAllDTO()
            {
                Organization = new BLLOrganizationDTO(organization.Id, organization.Name),
                Categories = categories.Select(dto => new BLLCategoryDTO(dto.Id, dto.Name, dto.ProductNames, dto.ChangeNames)).ToList(),
                Products = products.Select(dto => new BLLProductDTO(dto.Id, dto.Name, dto.CurrentPrice, dto.Categories)).ToList(),
                Changes = changes.Select(dto => new BLLChangeDTO(dto.Id, dto.Name, dto.CurrentPrice, dto.CategoryNames)).ToList()
            };
        }
    }
}