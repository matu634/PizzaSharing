using System;
using System.Linq;
using DAL.App.DTO;
using DAL.App.EF.Helpers;
using Domain;

namespace DAL.App.EF.Mappers
{
    public static class ChangeMapper
    {
        /// <summary>
        /// Maps Id, Name, Price, OrgId, CategoriesMin
        /// </summary>
        /// <param name="change"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static DALChangeDTO FromDomain(Change change)
        {
            if (change == null) throw new NullReferenceException("Can't map, Domain.Change is null");
            
            return new DALChangeDTO()
            {
                Id = change.Id,
                Name = change.ChangeName?.Translate() ?? "CHANGE NAME NOT LOADED",
                CurrentPrice = PriceFinder.ForChange(change, change.Prices, DateTime.Now) ?? -1.0m,
                OrganizationId = change.OrganizationId,
                Categories = change.ChangeInCategories? 
                    .Where(obj => obj.Category.IsDeleted == false)
                    .Select(obj => CategoryMapper.FromDomainToMin(obj.Category))
                    .ToList()
            };
        }

        /// <summary>
        /// Maps Id, Name, OrgId, Price
        /// </summary>
        /// <param name="change"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static DALChangeDTO FromDomain2(Change change, DateTime time)
        {
            if (change == null) throw new NullReferenceException("Can't map, change entity is null!");
            return new DALChangeDTO()
            {
                Name = change.ChangeName.Translate(),
                Id = change.Id,
                OrganizationId = change.OrganizationId,
                CurrentPrice = PriceFinder.ForChange(change, change.Prices, time) ?? decimal.MinusOne
            };
        }
        
        /// <summary>
        /// maps Id, Name
        /// </summary>
        /// <param name="change"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static DALChangeDTO FromDomainToMin(Change change)
        {
            if (change == null) throw new NullReferenceException("Can't map, Domain.Change is null");
            
            return new DALChangeDTO()
            {
                Name = change.ChangeName.Translate(),
                Id = change.Id,
            };
        }

        /// <summary>
        /// Only use for new db entities
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static Change FromDAL(DALChangeDTO dto)
        {
            return new Change()
            {
                OrganizationId = dto.OrganizationId,
                ChangeName = new MultiLangString(dto.Name),
            };
        }
    }
}