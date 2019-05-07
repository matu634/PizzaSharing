using System;
using System.Linq;
using DAL.App.DTO;
using Domain;

namespace DAL.App.EF.Mappers
{
    public static class CategoryMapper
    {
        public static DALCategoryDTO FromDomain(Category category)
        {
            if (category == null) throw new NullReferenceException("Can't map, category entity is null");
            return new DALCategoryDTO()
            {
                Id = category.Id,
                Name = category.CategoryName?.Translate() ?? "CATEGORY NAME NOT LOADED",
                Changes = category.ChangesInCategory?
                    .Where(obj => obj.Change.IsDeleted == false)
                    .Select(obj => ChangeMapper.FromDomainToMin(obj.Change))
                    .ToList(),
                Products = category.ProductsInCategory?
                    .Where(obj => obj.Product.IsDeleted == false)
                    .Select(obj => ProductMapper.FromDomain3(obj.Product))
                    .ToList()
            };
        }
        
        public static DALCategoryDTO FromDomain2(Category category)
        {
            if (category == null) throw new NullReferenceException("Can't map, category entity is null");
            return new DALCategoryDTO()
            {
                Id = category.Id,
                OrganizationId = category.OrganizationId,
                Name = category.CategoryName?.Translate() ?? "CATEGORY NAME NOT LOADED",
                Products = category.ProductsInCategory?
                    .Where(obj => !obj.Product.IsDeleted)
                    .Select(obj => ProductMapper.FromDomain2(obj.Product))
                    .ToList()
            };
        }
        
        public static DALCategoryDTO FromDomain3(Category category)
        {
            if (category == null) throw new NullReferenceException("Can't map, category entity is null");
            return new DALCategoryDTO()
            {
                Id = category.Id,
                OrganizationId = category.OrganizationId,
                Name = category.CategoryName?.Translate() ?? "CATEGORY NAME NOT LOADED",
            };
        }
        
        public static DALCategoryMinDTO FromDomainToMin(Category category)
        {
            if (category == null) throw new NullReferenceException("Can't map, category entity is null");
            return new DALCategoryMinDTO(category.Id, category.CategoryName?.Translate() ?? "CATEGORY NAME NOT LOADED"){};
        }
        
        public static Category FromDAL(DALCategoryDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, category DTO is null");
            return new Category()
            {
                CategoryName = new MultiLangString(dto.Name),
                OrganizationId = dto.OrganizationId
            };
        }
    }
}