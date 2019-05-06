using System;
using System.Linq;
using DAL.App.DTO;
using DAL.App.EF.Helpers;
using Domain;

namespace DAL.App.EF.Mappers
{
    public static class ChangeMapper
    {
        public static DALChangeDTO FromDomain(Change change)
        {
            if (change == null) throw new NullReferenceException("Can't map, Domain.Change is null");
            
            return new DALChangeDTO()
            {
                Id = change.Id,
                Name = change.ChangeName?.Translate(),
                CurrentPrice = PriceFinder.ForChange(change, change.Prices, DateTime.Now) ?? -1.0m,
                OrganizationId = change.OrganizationId,
                Categories = change.ChangeInCategories?
                    .Where(obj => obj.Category.IsDeleted == false)
                    .Select(obj => CategoryMapper.FromDomainToMin(obj.Category))
                    .ToList()
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