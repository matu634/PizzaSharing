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
                ChangeNames = category.ChangesInCategory?
                    .Where(obj => obj.Change.IsDeleted == false)
                    .Select(obj => obj.Change.ChangeName.Translate())
                    .ToList(),
                ProductNames = category.ProductsInCategory?
                    .Where(obj => obj.Product.IsDeleted == false)
                    .Select(obj => obj.Product.ProductName.Translate())
                    .ToList()
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