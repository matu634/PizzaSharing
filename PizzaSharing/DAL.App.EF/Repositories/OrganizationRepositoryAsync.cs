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

        public async Task<List<OrganizationDTO>> AllDTOAsync(DateTime time)
        {
            //TODO: optimize this
            var o = await RepoDbSet
                .Include(organization => organization.Categories)
                .ThenInclude(category => category.CategoryName)
                .ThenInclude(name => name.Translations)
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
                
                
                return o.Select(organization => new OrganizationDTO()
                {
                    Id = organization.Id,
                    Name = organization.OrganizationName,
                    Categories = organization.Categories
                        .Where(category => category.IsDeleted == false)
                        .Select(category => new CategoryDTO()
                        {
                            Id = category.Id,
                            Name = category.CategoryName.Translate(),
                            Products = category.ProductsInCategory
                                .Where(inCategory => inCategory.Product.IsDeleted == false && 
                                                     inCategory.Product.Prices.Any(p => p.ValidTo > time && p.ValidFrom < time))
                                .Select(inCategory => new ProductDTO()
                                {
                                    ProductId = inCategory.ProductId,
                                    ProductName = inCategory.Product.ProductName.Translate(),
                                    ProductPrice = inCategory.Product.Prices.Where(p => p.ValidTo > time && p.ValidFrom < time).ToList()[0].Value
                                })
                                .ToList()
                        })
                        .ToList()
                })
                .ToList();
        }

        public async Task<List<DALOrganizationMinDTO>> AllMinDTOAsync()
        {
            return await RepoDbSet
                .Where(organization => organization.IsDeleted == false)
                .Select(organization => new DALOrganizationMinDTO(organization.Id, organization.OrganizationName))
                .ToListAsync();
        }

        public async Task<DALOrganizationMinDTO> FindMinDTOAsync(int id)
        {
            var organization =  await RepoDbSet.FindAsync(id);
            if (organization == null || organization.IsDeleted) return null;
            
            return new DALOrganizationMinDTO(organization.Id, organization.OrganizationName);
        }

        public async Task<DALOrganizationDTO> FindWithCategoriesAsync(int id)
        {
            var organization = await RepoDbSet
                .Include(o => o.Categories)
                .ThenInclude(category => category.CategoryName)
                .ThenInclude(name => name.Translations)
                .FirstOrDefaultAsync(o => o.IsDeleted == false && o.Id == id);
            if (organization == null) return null;
            
            return new DALOrganizationDTO()
            {
                Id = organization.Id,
                Name = organization.OrganizationName,
                Categories = organization.Categories.Select(CategoryMapper.FromDomain).ToList()
            };
        }

        public async Task AddAsync(DALOrganizationMinDTO organizationDTO)
        {
            var organization = new Organization()
            {
                OrganizationName = organizationDTO.Name
            };

            await RepoDbSet.AddAsync(organization);
        }
    }
}