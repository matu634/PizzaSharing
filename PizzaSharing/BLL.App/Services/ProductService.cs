using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.masirg.BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ChangeMapper = BLL.App.Mappers.ChangeMapper;
using ProductMapper = BLL.App.Mappers.ProductMapper;

namespace BLL.App.Services
{
    public class ProductService : BaseService<IAppUnitOfWork>, IProductService
    {
        public ProductService(IAppUnitOfWork uow) : base(uow)
        {
        }

        public async Task<bool> AddProductAsync(BLLProductDTO productDTO)
        {
            var organization = await Uow.Organizations.FindWithCategoriesAsync(productDTO.OrganizationId);
            if (organization?.Categories == null || organization.Categories.Count == 0) return false;

            var organizationCategoryIds = organization.Categories.Select(dto => dto.Id).ToList();

            foreach (var category in productDTO.Categories)
            {
                if (!organizationCategoryIds.Contains(category.Id)) return false;
            }

            //1.Add product
            var product = await Uow.Products.AddAsync(ProductMapper.FromBLL(productDTO));

            //2. Add product categories
            foreach (var category in productDTO.Categories)
            {
                await Uow.ProductsInCategories.AddAsync(product.Id, category.Id);
            }
            
            //3. Add price
            var priceDTO = new DALPriceDTO()
            {
                Value = productDTO.CurrentPrice,
                ProductId = product.Id,
                ValidFrom = DateTime.MinValue,
                ValidTo = DateTime.MaxValue,
            };
            await Uow.Prices.AddAsync(priceDTO);
            await Uow.SaveChangesAsync();
            return true;
        }

        public async Task<BLLProductDTO> GetProductAsync(int productId)
        {
            var productDto = await Uow.Products.FindDTOAsync(productId);
            if (productDto == null) return null;

            return ProductMapper.FromDAL(productDto); 
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var result = await Uow.Products.RemoveSoft(productId);
            if (result == false) return false;
            await Uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditProduct(BLLProductDTO productDTO)
        {
            var organization = await Uow.Organizations.FindWithCategoriesAsync(productDTO.OrganizationId);
            if (organization?.Categories == null || organization.Categories.Count == 0) return false;

            var organizationCategoryIds = organization.Categories.Select(dto => dto.Id).ToList();

            foreach (var category in productDTO.Categories)
            {
                if (!organizationCategoryIds.Contains(category.Id)) return false;
            }

            //1.Edit product entity
            var product = await Uow.Products.EditAsync(ProductMapper.FromBLL2(productDTO));
            if (product == null) return false;
            
            //2. Edit product categories
            await Uow.ProductsInCategories.RemoveByProductId(product.Id);
            
            foreach (var category in productDTO.Categories)
            {
                await Uow.ProductsInCategories.AddAsync(product.Id, category.Id);
            }
            
            //3. Add price
            var priceDTO = new DALPriceDTO()
            {
                Value = productDTO.CurrentPrice,
                ProductId = product.Id,
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.MaxValue,
            };
            await Uow.Prices.EditAsync(priceDTO);
            await Uow.SaveChangesAsync();
            return true;
        }

        public async Task<List<BLLChangeDTO>> GetProductChangesAsync(int productId)
        {
            var product = await Uow.Products.FindDTOAsync(productId);
            if (product == null) return null;
            var categoryIds = product.Categories.Select(dto => dto.Id).ToArray();

            var changes = await Uow.ChangesInCategories.GetChangesByCategoryIdsAsync(categoryIds);

            if (changes == null) return null;

            return changes
                .Select(ChangeMapper.FromDAL2)
                .ToList();
        }
    }
}