using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.DAL.App.Repositories;
using ee.itcollege.masirg.Contracts.DAL.Base;
using DAL.App.DTO;
using DAL.App.EF.Helpers;
using DAL.App.EF.Mappers;
using ee.itcollege.masirg.DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ProductRepositoryAsync : BaseRepositoryAsync<Product>, IProductRepository
    {
        public ProductRepositoryAsync(IDataContext dataContext) : base(dataContext)
        {
        }
        
        public async Task<List<DALProductDTO>> AllAsync(int organizationId)
        {
            var products = await RepoDbSet
                .Include(product => product.ProductName)
                .ThenInclude(name => name.Translations)
                .Include(product => product.ProductDescription)
                .ThenInclude(description => description.Translations)
                .Include(product => product.Prices)
                .Include(product => product.ProductInCategories)
                .ThenInclude(obj => obj.Category)
                .ThenInclude(category => category.CategoryName)
                .ThenInclude(name => name.Translations)
                .Where(product => product.IsDeleted == false && product.OrganizationId == organizationId)
                .ToListAsync();

            return products
                    .Select(ProductMapper.FromDomain)
                    .Where(dto => dto.CurrentPrice != -1.0m)
                    .ToList();
        }

        public async Task<DALProductDTO> AddAsync(DALProductDTO dto)
        {
            var product = ProductMapper.FromDAL(dto);

            product = (await RepoDbSet.AddAsync(product)).Entity;
            if (product == null) return null;
            await RepoDbContext.Entry(product).Reference(p => p.ProductName).LoadAsync();
            await RepoDbContext.Entry(product.ProductName).Collection( name => name.Translations).LoadAsync();
            
            await RepoDbContext.Entry(product).Reference(p => p.ProductDescription).LoadAsync();
            await RepoDbContext.Entry(product.ProductDescription).Collection( desc => desc.Translations).LoadAsync();


            return ProductMapper.FromDomain(product);
        }

        public async Task<DALProductDTO> FindDTOAsync(int productId)
        {
            var product = await RepoDbSet
                .Include(p => p.ProductName)
                .ThenInclude(p => p.Translations)
                .Include(p => p.ProductDescription)
                .ThenInclude(desc => desc.Translations)
                .Include(p => p.ProductInCategories)
                .ThenInclude(obj => obj.Category)
                .ThenInclude(category => category.CategoryName)
                .ThenInclude(name => name.Translations)
                .Include(p => p.Prices)
                .Where(p => p.IsDeleted == false && p.Id == productId)
                .SingleOrDefaultAsync();
            if (product == null) return null;

            var currentPrice = PriceFinder.ForProduct(product, product.Prices, DateTime.Now);
            if (currentPrice == null) return null;

            return ProductMapper.FromDomain(product);
        }

        public async Task<bool> RemoveSoft(int productId)
        {
            var product = await RepoDbSet.FindAsync(productId);
            if (product == null) return false;
            product.IsDeleted = true;
            return true;
        }

        /// <summary>
        /// Edits products name and description, then returns productDTO
        /// </summary>
        /// <param name="dalProductDTO"></param>
        /// <returns></returns>
        public async Task<DALProductDTO> EditAsync(DALProductDTO dalProductDTO)
        {
            var product = await RepoDbSet.FindAsync(dalProductDTO.Id);
            
            if (product == null) return null;
            await RepoDbContext.Entry(product).Reference(p => p.ProductName).LoadAsync();
            await RepoDbContext.Entry(product.ProductName).Collection( name => name.Translations).LoadAsync();
            
            await RepoDbContext.Entry(product).Reference(p => p.ProductDescription).LoadAsync();
            await RepoDbContext.Entry(product.ProductDescription).Collection( desc => desc.Translations).LoadAsync();
            
            product.ProductDescription.SetTranslation(dalProductDTO.Description);
            product.ProductName.SetTranslation(dalProductDTO.Name);

            return ProductMapper.FromDomain(product);
        }
    }
}