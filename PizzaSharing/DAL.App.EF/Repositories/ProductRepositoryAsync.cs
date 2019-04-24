using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.App.DTO;
using DAL.App.EF.Helpers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ProductRepositoryAsync : BaseRepositoryAsync<Product>, IProductRepository
    {
        public ProductRepositoryAsync(IDataContext dataContext) : base(dataContext)
        {
        }

        public override async Task<IEnumerable<Product>> AllAsync()
        {
            return await RepoDbSet
                .Include(obj => obj.Organization)
                .ToListAsync();
        }

        public override async Task<Product> FindAsync(params object[] id)
        {
            var productInCategory = await RepoDbSet.FindAsync(id);

            if (productInCategory != null)
            {
                await RepoDbContext.Entry(productInCategory).Reference(obj => obj.Organization).LoadAsync();
            }

            return productInCategory;
        }

        public async Task<List<DALProductDTO>> AllAsync(int organizationId)
        {
            var products = await RepoDbSet
                .Include(product => product.Prices)
                .Include(product => product.ProductInCategories)
                .ThenInclude(obj => obj.Category)
                .Where(product => product.IsDeleted == false && product.OrganizationId == organizationId)
                .ToListAsync();

            return products
                .Select(product => new DALProductDTO()
                {
                    Id = product.Id,
                    Name = product.ProductName,
                    CurrentPrice = PriceFinder.ForProduct(product, product.Prices, DateTime.Now) ?? -1.0m,
                    Categories = product.ProductInCategories
                        .Where(obj => obj.Category.IsDeleted == false)
                        .Select(obj => new DALCategoryMinDTO(obj.Category.Id, obj.Category.CategoryName))
                        .ToList()
                })
                .Where(dto => dto.CurrentPrice != -1.0m)
                .ToList();
        }

        public async Task<DALProductDTO> AddAsync(DALProductDTO dto)
        {
            var product = new Product()
            {
                OrganizationId = dto.OrganizationId,
                ProductName = dto.Name
            };

            product = (await RepoDbSet.AddAsync(product)).Entity;

            return new DALProductDTO()
            {
                Id = product.Id,
                Name = product.ProductName,
                OrganizationId = product.OrganizationId
            };
        }

        public async Task<DALProductDTO> FindDTOAsync(int productId)
        {
            var product = await RepoDbSet
                .Include(p => p.ProductInCategories)
                .ThenInclude(obj => obj.Category)
                .Include(p => p.Prices)
                .Where(p => p.IsDeleted == false && p.Id == productId)
                .SingleOrDefaultAsync();
            if (product == null) return null;

            var currentPrice = PriceFinder.ForProduct(product, product.Prices, DateTime.Now);
            if (currentPrice == null) return null;

            return new DALProductDTO()
            {
                Id = product.Id,
                Name = product.ProductName,
                CurrentPrice = currentPrice.Value,
                OrganizationId = product.OrganizationId,
                Categories = product.ProductInCategories
                    .Select(obj => new DALCategoryMinDTO(obj.CategoryId, obj.Category.CategoryName))
                    .ToList()
            };
        }

        public async Task<bool> RemoveSoft(int productId)
        {
            var product = await RepoDbSet.FindAsync(productId);
            if (product == null) return false;
            product.IsDeleted = true;
            return true;
        }

        public async Task<DALProductDTO> EditAsync(DALProductDTO dalProductDTO)
        {
            var product = await RepoDbSet.FindAsync(dalProductDTO.Id);
            if (product == null) return null;

            //TODO: descirption change
            product.ProductName = dalProductDTO.Name;
            
            return new DALProductDTO()
            {
                Id = product.Id,
                Name = product.ProductName,
                OrganizationId = product.OrganizationId
            };
        }
    }
}