using System;
using System.Linq;
using BLL.App.DTO;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public static class CategoryMapper
    {
        /// <summary>
        /// Maps Id, Name, OrganizationId, Products(name, id), Changes(name, id)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static BLLCategoryDTO FromDAL(DALCategoryDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, DALCategoryDTO is null");
            return new BLLCategoryDTO()
            {
                CategoryId = dto.Id,
                OrganizationId = dto.OrganizationId,
                CategoryName = dto.Name,
                Products = dto.Products
                    .Select(ProductMapper.FromDAL3)
                    .ToList(),
                Changes = dto.Changes
                    .Select(ChangeMapper.FromDAL3)
                    .ToList()
            };
        }

        /// <summary>
        /// Maps Id, Name, OrganizationId, Products(name, id, orgId, price, description)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static BLLCategoryDTO FromDAL2(DALCategoryDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, DALCategoryDTO is null");
            return new BLLCategoryDTO()
            {
                CategoryId = dto.Id,
                OrganizationId = dto.OrganizationId,
                CategoryName = dto.Name,
                Products = dto.Products
                    .Select(ProductMapper.FromDAL2)
                    .ToList()
            };
        }

        /// <summary>
        /// Maps id, name
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static BLLCategoryDTO FromDAL3(DALCategoryDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, DALCategoryDTO is null");
            return new BLLCategoryDTO()
            {
                CategoryId = dto.Id,
                CategoryName = dto.Name
            };
        }

        /// <summary>
        /// Maps Min
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static BLLCategoryMinDTO FromDALToMin(DALCategoryMinDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, DALCategoryDTO is null");
            return new BLLCategoryMinDTO()
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }

        /// <summary>
        /// Maps Name, OrganizationId
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static DALCategoryDTO FromBLL(BLLCategoryDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, BLLCategoryDTO is null");
            return new DALCategoryDTO()
            {
                Name = dto.CategoryName,
                OrganizationId = dto.OrganizationId
            };
        }
    }
}