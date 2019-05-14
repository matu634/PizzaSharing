using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using ee.itcollege.masirg.BLL.Base.Services;
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
            var dtos = await Uow.Organizations.AllMinDTOAsync();

            return dtos
                .Select(OrganizationMapper.FromDAL2)
                .ToList();
        }

        public async Task<BLLOrganizationDTO> GetOrganizationAllDTOAsync(int id)
        {
            var organization = await Uow.Organizations.FindMinDTOAsync(id);
            if (organization == null) return null;

            var categories = await Uow.Categories.AllAsync(organizationId: id);
            var products = await Uow.Products.AllAsync(organizationId: id);
            var changes = await Uow.Changes.AllAsync(organizationId: id);

            return OrganizationMapper.FromDAL3(organization, categories, products, changes);
        }

        public async Task<BLLOrganizationDTO> GetOrganizationWithCategoriesAsync(int organizationId)
        {
            var organization = await Uow.Organizations.FindWithCategoriesAsync(organizationId);

            if (organization?.Categories == null || organization.Categories.Count == 0) return null;

            return OrganizationMapper.FromDAL4(organization);
        }

        public async Task<BLLOrganizationDTO> GetOrganizationMinAsync(int organizationId)
        {
            var organization = await Uow.Organizations.FindMinDTOAsync(organizationId);
            if (organization == null) return null;
            return OrganizationMapper.FromDAL2(organization);

        }

        public async Task<bool> AddOrganizationAsync(BLLOrganizationDTO organization)
        {

            await Uow.Organizations.AddAsync(OrganizationMapper.FromBLL(organization));
            await Uow.SaveChangesAsync();
            return true;
        }
    }
}