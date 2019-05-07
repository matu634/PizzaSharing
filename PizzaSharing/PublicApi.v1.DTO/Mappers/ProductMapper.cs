using System;
using BLL.App.DTO;

namespace PublicApi.DTO.Mappers
{
    public static class ProductMapper
    {
        /// <summary>
        /// Maps id, name, price, desc
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static ProductDTO FromBLL(BLLProductDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, BLLCategoryDTO is null");
            return new ProductDTO()
            {
                Description = dto.Description,
                ProductId = dto.Id,
                ProductName = dto.ProductName,
                ProductPrice = dto.CurrentPrice
                
            };
            
        }
    }
}