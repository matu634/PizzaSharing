using System;
using DAL.App.DTO;
using Domain.Identity;

namespace DAL.App.EF.Mappers
{
    public static class AppUserMapper
    {
        public static DALAppUserDTO FromDomain(AppUser entity)
        {
            if (entity == null) throw new NullReferenceException("Can't map, AppUser entity is null");
            return new DALAppUserDTO()
            {
                Id = entity.Id,
                UserNickname = entity.UserNickname
            };
        }
    }
}