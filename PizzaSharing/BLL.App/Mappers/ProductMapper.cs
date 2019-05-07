using System;
using System.Linq;
using BLL.App.DTO;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public static class ProductMapper
    {
        /// <summary>
        /// Maps: Id, Price, Name, Description, OrganizationId, CategoriesMin
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static BLLProductDTO FromDAL(DALProductDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, DALProductDTO is null");
            return new BLLProductDTO()
            {
                Id = dto.Id,
                CurrentPrice = dto.CurrentPrice,
                ProductName = dto.Name,
                Description = dto.Description,
                OrganizationId = dto.OrganizationId,
                Categories = dto.Categories
                    .Select(CategoryMapper.FromDALToMin)
                    .ToList()
            };
        }
        
        
        /// <summary>
        /// Maps: Id, Price, Name, Description, OrganizationId
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static BLLProductDTO FromDAL2(DALProductDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, DALProductDTO is null");
            return new BLLProductDTO()
            {
                Id = dto.Id,
                CurrentPrice = dto.CurrentPrice,
                ProductName = dto.Name,
                Description = dto.Description,
                OrganizationId = dto.OrganizationId,
            };
        }
        
        /// <summary>
        /// Maps: Id, Name
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static BLLProductDTO FromDAL3(DALProductDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, DALProductDTO is null");
            return new BLLProductDTO()
            {
                Id = dto.Id,
                ProductName = dto.Name
            };
        }
    }
}