using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.App.DTO;
using DAL.App.EF.Helpers;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO;

namespace DAL.App.EF.Repositories
{
    public class OrganizationRepositoryAsync : BaseRepositoryAsync<Organization>, IOrganizationRepository
    {
        public OrganizationRepositoryAsync(IDataContext dataContext) : base(dataContext)
        {
        }

        public async Task<List<DALOrganizationDTO>> AllWithCategoriesAndProducts(DateTime time)
        {
            var o = await RepoDbSet
                .Include(organization => organization.Categories)
                .ThenInclude(category => category.CategoryName)
                .ThenInclude(name => name.Translations)
                .Include(organization => organization.Categories)
                .ThenInclude(category => category.ProductsInCategory)
                .ThenInclude(obj => obj.Product)
                .ThenInclude(product => product.ProductDescription)
                .ThenInclude(desc => desc.Translations)
                .Include(organization => organization.Categories)
                .ThenInclude(category => category.ProductsInCategory)
                .ThenInclude(obj => obj.Product)
                .ThenInclude(product => product.ProductName)
                .ThenInclude(name => name.Translations)
                .Include(organization => organization.Categories)
                .ThenInclude(category => category.ProductsInCategory)
                .ThenInclude(obj => obj.Product)
                .ThenInclude(product => product.Prices)
                .Where(organization => organization.IsDeleted == false)
                .ToListAsync();

            return o.Select(OrganizationMapper.FromDomain).ToList();
        }

        public async Task<List<DALOrganizationDTO>> AllMinDTOAsync()
        {
            return (await RepoDbSet
                .Where(organization => organization.IsDeleted == false)
                .ToListAsync())
                .Select(OrganizationMapper.FromDomain2)
                .ToList();
        }

        public async Task<DALOrganizationDTO> FindMinDTOAsync(int id)
        {
            var organization =  await RepoDbSet.FindAsync(id);
            if (organization == null || organization.IsDeleted) return null;

            return OrganizationMapper.FromDomain2(organization);
        }

        public async Task<DALOrganizationDTO> FindWithCategoriesAsync(int id)
        {
            var organization = await RepoDbSet
                .Include(o => o.Categories)
                .ThenInclude(category => category.CategoryName)
                .ThenInclude(name => name.Translations)
                .FirstOrDefaultAsync(o => o.IsDeleted == false && o.Id == id);
            if (organization == null) return null;

            return OrganizationMapper.FromDomain3(organization);
        }

        public async Task AddAsync(DALOrganizationDTO organizationDTO)
        {

            await RepoDbSet.AddAsync(OrganizationMapper.FromDAL(organizationDTO));
        }
    }
}