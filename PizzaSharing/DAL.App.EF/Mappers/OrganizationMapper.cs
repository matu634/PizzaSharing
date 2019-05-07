using System;
using System.Linq;
using DAL.App.DTO;
using Domain;

namespace DAL.App.EF.Mappers
{
    public static class OrganizationMapper
    {
        /// <summary>
        /// Maps Id, name, categories with products
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static DALOrganizationDTO FromDomain(Organization organization)
        {
            if (organization == null) throw new NullReferenceException("Can't map, organization domain entity is null");
            return new DALOrganizationDTO()
            {
                Id = organization.Id,
                Name = organization.OrganizationName,
                Categories = organization.Categories?
                    .Where(category => !category.IsDeleted)
                    .Select(CategoryMapper.FromDomain2)
                    .ToList()
            };
        }
        
        /// <summary>
        /// Maps id and name
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static DALOrganizationDTO FromDomain2(Organization organization)
        {
            if (organization == null) throw new NullReferenceException("Can't map, organization domain entity is null");
            return new DALOrganizationDTO()
            {
                Id = organization.Id,
                Name = organization.OrganizationName,
            };
        }
        
        /// <summary>
        ///  Maps Id, name, categories
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static DALOrganizationDTO FromDomain3(Organization organization)
        {
            if (organization == null) throw new NullReferenceException("Can't map, organization domain entity is null");
            return new DALOrganizationDTO()
            {
                Id = organization.Id,
                Name = organization.OrganizationName,
                Categories = organization.Categories?
                    .Where(category => !category.IsDeleted)
                    .Select(CategoryMapper.FromDomain3)
                    .ToList()
            };
        }

        /// <summary>
        ///  Maps name
        /// </summary>
        /// <param name="organizationDTO"></param>
        /// <returns></returns>
        public static Organization FromDAL(DALOrganizationDTO organizationDTO)
        {
            return new Organization()
            {
                OrganizationName = organizationDTO.Name
            };
        }
    }
}