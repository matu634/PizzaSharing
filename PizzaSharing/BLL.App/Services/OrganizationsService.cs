using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using DAL.App.DTO;
using Microsoft.EntityFrameworkCore.Internal;

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
                Categories = categories
                    .Select(dto => new BLLCategoryDTO(dto.Id, dto.Name, dto.ProductNames, dto.ChangeNames))
                    .ToList(),
                Products = products
                    .Select(dto => new BLLProductDTO(dto.Id, dto.Name, dto.CurrentPrice, dto.Categories
                        .Select(minDTO => new BLLCategoryMinDTO(minDTO.Id, minDTO.Name))
                        .ToList()))
                    .ToList(),
                Changes = changes
                    .Select(dto => new BLLChangeDTO(dto.Id, dto.Name, dto.CurrentPrice, dto.Categories
                            .Select(minDTO => new BLLCategoryMinDTO(minDTO.Id, minDTO.Name))
                            .ToList()))
                    .ToList()
            };
        }

        public async Task<BLLOrganizationWithCategoriesDTO> GetOrganizationWithCategoriesAsync(int organizationId)
        {
            var organization = await Uow.Organizations.FindWithCategoriesAsync(organizationId);

            if (organization?.Categories == null || organization.Categories.Count == 0) return null;

            return new BLLOrganizationWithCategoriesDTO()
            {
                Id = organization.Id,
                Name = organization.Name,
                Categories = organization.Categories.Select(dto => new BLLCategoryMinDTO(dto.Id, dto.Name)).ToList()
            };
        }
    }
}