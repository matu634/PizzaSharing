using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.App.DTO;
using DAL.App.EF.Helpers;
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

        public async Task<List<OrganizationDTO>> AllDtoAsync(DateTime time)
        {
            //TODO: optimize this
            return await RepoDbSet
                .Include(organization => organization.Categories)
                .ThenInclude(category => category.ProductsInCategory)
                .ThenInclude(obj => obj.Product)
                .ThenInclude(product => product.Prices)
                .Where(organization => organization.IsDeleted == false)
                .Select(organization => new OrganizationDTO()
                {
                    Id = organization.Id,
                    Name = organization.OrganizationName,
                    Categories = organization.Categories
                        .Where(category => category.IsDeleted == false)
                        .Select(category => new CategoryDTO()
                        {
                            Id = category.Id,
                            Name = category.CategoryName,
                            Products = category.ProductsInCategory
                                .Where(inCategory => inCategory.Product.IsDeleted == false && 
                                                     inCategory.Product.Prices.Any(p => p.ValidTo > time && p.ValidFrom < time))
                                .Select(inCategory => new ProductDTO()
                                {
                                    ProductId = inCategory.ProductId,
                                    ProductName = inCategory.Product.ProductName,
                                    ProductPrice = inCategory.Product.Prices.Where(p => p.ValidTo > time && p.ValidFrom < time).ToList()[0].Value
                                })
                                .ToList()
                        })
                        .ToList()
                })
                .ToListAsync();
        }

        public async Task<List<DALOrganizationMinDTO>> AllDtoMinAsync()
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
                .FirstOrDefaultAsync(o => o.IsDeleted == false && o.Id == id);
            if (organization == null) return null;
            
            return new DALOrganizationDTO()
            {
                Id = organization.Id,
                Name = organization.OrganizationName,
                Categories = organization.Categories.Select(category => new DALCategoryDTO()
                {
                    Id = category.Id,
                    Name = category.CategoryName
                }).ToList()
            };
        }
    }
}