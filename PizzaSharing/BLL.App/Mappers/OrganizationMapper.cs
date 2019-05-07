using System;
using System.Collections.Generic;
using System.Linq;
using BLL.App.DTO;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public static class OrganizationMapper
    {
        /// <summary>
        /// Maps Id, Name, Categories with products
        /// </summary>
        /// <returns></returns>
        public static BLLOrganizationDTO FromDAL(DALOrganizationDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, DALOrganizationDTO is null");
            return new BLLOrganizationDTO()
            {
                Id = dto.Id,
                Name = dto.Name,
                Categories = dto.Categories.Select(CategoryMapper.FromDAL2).ToList()
            };
        }


        /// <summary>
        /// Maps Id, Name
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static BLLOrganizationDTO FromDAL2(DALOrganizationDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, DALOrganizationDTO is null");
            return new BLLOrganizationDTO()
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }

        /// <summary>
        /// Maps Id, Name, Categories from param, Products from param, Changes from param
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="categories"></param>
        /// <param name="products"></param>
        /// <param name="changes"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static BLLOrganizationDTO FromDAL3(
            DALOrganizationDTO organization, 
            List<DALCategoryDTO> categories, 
            List<DALProductDTO> products, 
            List<DALChangeDTO> changes)
        {
            if (organization == null) throw new NullReferenceException("Can't map, DALOrganizationDTO is null");
            return new BLLOrganizationDTO()
            {
                Id = organization.Id,
                Name = organization.Name,
                Categories = categories
                    .Select(CategoryMapper.FromDAL)
                    .ToList(),
                Changes = changes
                    .Select(ChangeMapper.FromDAL)
                    .ToList(),
                Products = products
                    .Select(ProductMapper.FromDAL)
                    .ToList()
            };
        }
        /// <summary>
        /// Maps id, name, Categories(id, name)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static BLLOrganizationDTO FromDAL4(DALOrganizationDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, DALOrganizationDTO is null");
            return new BLLOrganizationDTO()
            {
                Id = dto.Id,
                Name = dto.Name,
                Categories = dto.Categories
                    .Select(CategoryMapper.FromDAL3)
                    .ToList()
            };
        }

        /// <summary>
        /// Maps name
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static DALOrganizationDTO FromBLL(BLLOrganizationDTO dto)
        {
            return new DALOrganizationDTO(){Name = dto.Name};
        }
    }
}