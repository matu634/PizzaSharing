using System;
using System.Linq;
using BLL.App.DTO;

namespace PublicApi.DTO.Mappers
{
    public class OrganizationMapper
    {
        /// <summary>
        /// Maps id, name, Categories(id, name, Products(id, name, desc, price))
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static OrganizationDTO FromBLL(BLLOrganizationDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, BLLOrganizationDTO is null");
            return new OrganizationDTO()
            {
                Id = dto.Id,
                Name = dto.Name,
                Categories = dto.Categories
                    .Select(CategoryMapper.FromBLL)
                    .ToList()
            };
        }
    }
}