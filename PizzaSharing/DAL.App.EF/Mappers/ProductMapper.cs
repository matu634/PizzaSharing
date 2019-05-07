using System;
using System.Collections.Specialized;
using System.Linq;
using DAL.App.DTO;
using DAL.App.EF.Helpers;
using Domain;

namespace DAL.App.EF.Mappers
{
    public static class ProductMapper
    {
        /// <summary>
        /// Maps id, price, name, desc, orgId, CategoriesMin
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static DALProductDTO FromDomain(Product product)
        {
            if (product == null) throw new NullReferenceException("Can't use Mapper on a null object");
            
            return new DALProductDTO()
            {
                Id = product.Id,
                CurrentPrice = PriceFinder.ForProduct(product, product.Prices, DateTime.Now) ?? -1.0m,
                Name = product.ProductName?.Translate() ?? "PRODUCT NAME NOT LOADED",
                Description = product.ProductDescription?.Translate() ?? "PRODUCT DESCRIPTION NOT LOADED",
                OrganizationId = product.OrganizationId,
                Categories = product.ProductInCategories != null && product.ProductInCategories.Any() ?
                    product.ProductInCategories
                        .Where(obj => !obj.Category.IsDeleted)
                        .Select(obj => CategoryMapper.FromDomainToMin(obj.Category))
                        .ToList():
                        null
            };
        }

        /// <summary>
        /// Maps id, name, desc, price, orgId
        /// </summary>
        /// <param name="product"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static DALProductDTO FromDomain2(Product product)
        {
            if (product == null) throw new NullReferenceException("Can't use Mapper on a null object");
            
            return new DALProductDTO()
            {
                Id = product.Id,
                CurrentPrice = PriceFinder.ForProduct(product, product.Prices, DateTime.Now) ?? -1.0m,
                Name = product.ProductName?.Translate() ?? "PRODUCT NAME NOT LOADED",
                Description = product.ProductDescription?.Translate() ?? "PRODUCT DESCRIPTION NOT LOADED",
                OrganizationId = product.OrganizationId,
            };
        }
        
        /// <summary>
        /// Maps id, name, desc, price(at given time), orgId
        /// </summary>
        /// <param name="product"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static DALProductDTO FromDomain2(Product product, DateTime time)
        {
            if (product == null) throw new NullReferenceException("Can't use Mapper on a null object");
            
            return new DALProductDTO()
            {
                Id = product.Id,
                CurrentPrice = PriceFinder.ForProduct(product, product.Prices, time) ?? -1.0m,
                Name = product.ProductName?.Translate() ?? "PRODUCT NAME NOT LOADED",
                Description = product.ProductDescription?.Translate() ?? "PRODUCT DESCRIPTION NOT LOADED",
                OrganizationId = product.OrganizationId,
            };
        }
        
        /// <summary>
        /// Maps id, name, desc, orgID
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static DALProductDTO FromDomain3(Product product)
        {
            if (product == null) throw new NullReferenceException("Can't use Mapper on a null object");
            
            return new DALProductDTO()
            {
                Id = product.Id,
                Name = product.ProductName?.Translate() ?? "PRODUCT NAME NOT LOADED",
                Description = product.ProductDescription?.Translate() ?? "PRODUCT DESCRIPTION NOT LOADED",
                OrganizationId = product.OrganizationId,
            };
        }

        /// <summary>
        /// Only use when mapping new product
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static Product FromDAL(DALProductDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't use Mapper on a null object");
            return new Product()
            {
                OrganizationId = dto.OrganizationId,
                ProductName = new MultiLangString(dto.Name),
                ProductDescription = new MultiLangString(dto.Description)
            };
        }
    }
}