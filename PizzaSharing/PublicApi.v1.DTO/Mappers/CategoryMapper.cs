using System;
using System.Linq;
using BLL.App.DTO;

namespace PublicApi.DTO.Mappers
{
    public static class CategoryMapper
    {
        /// <summary>
        /// Maps id, name, Products(id, name, price, description)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static CategoryDTO FromBLL(BLLCategoryDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, BLLCategoryDTO is null");
            return new CategoryDTO()
            {
                Id = dto.CategoryId,
                Name =  dto.CategoryName,
                Products = dto.Products
                    .Select(ProductMapper.FromBLL)
                    .ToList()
            };
            
        }
    }
}