using System;
using BLL.App.DTO;

namespace PublicApi.DTO.Mappers
{
    public class AppUserMapper
    {
        /// <summary>
        /// Maps id, name
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static AppUserDTO FromBLL(BLLAppUserDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, BLLAppUserDTO is null");
            
            return new AppUserDTO()
            {
                Id = dto.Id,
                Name = dto.Nickname
            };
        }
    }
}