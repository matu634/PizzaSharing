using System;
using System.Linq;
using BLL.App.DTO;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public static class ChangeMapper
    {
        /// <summary>
        ///  Maps Id, Name
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static BLLChangeDTO FromDAL3(DALChangeDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, DALChangeDTO is null");
            
            return new BLLChangeDTO()
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }
        
        /// <summary>
        /// Maps id, name, price, orgId, CategoriesMin
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static BLLChangeDTO FromDAL(DALChangeDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, DALChangeDTO is null");
            
            return new BLLChangeDTO()
            {
                Id = dto.Id,
                Name = dto.Name,
                Categories = dto.Categories
                    .Select(CategoryMapper.FromDALToMin)
                    .ToList(),
                CurrentPrice = dto.CurrentPrice,
                OrganizationId = dto.OrganizationId
            };
        }
        
        /// <summary>
        /// Maps id, name, price, orgId
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static BLLChangeDTO FromDAL2(DALChangeDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, DALChangeDTO is null");
            
            return new BLLChangeDTO()
            {
                Id = dto.Id,
                Name = dto.Name,
                CurrentPrice = dto.CurrentPrice,
                OrganizationId = dto.OrganizationId
            };
        }
    }
}