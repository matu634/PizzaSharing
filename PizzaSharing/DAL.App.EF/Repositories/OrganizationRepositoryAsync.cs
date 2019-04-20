using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
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
                .Select(organization => new OrganizationDTO()
                {
                    Id = organization.Id,
                    Name = organization.OrganizationName,
                    Categories = organization.Categories.Select(category => new CategoryDTO()
                        {
                            Id = category.Id,
                            Name = category.CategoryName,
                            Products = category.ProductsInCategory.Select(inCategory => new ProductDTO()
                                {
                                    ProductId = inCategory.ProductId,
                                    ProductName = inCategory.Product.ProductName,
                                    ProductPrice = inCategory.Product.Prices.FirstOrDefault(p =>
                                        p.ValidTo.Ticks > time.Ticks && p.ValidFrom.Ticks < time.Ticks).Value
                                })
                                .Where(dto => dto.ProductPrice != null)
                                .ToList()
                        })
                        .ToList()
                })
                .ToListAsync();
        }
    }
}