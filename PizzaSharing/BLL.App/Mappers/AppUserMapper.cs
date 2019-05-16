using System;
using BLL.App.DTO;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class AppUserMapper
    {
        public static BLLAppUserDTO FromDALParticipantDTO(DALRowParticipantDTO participantDTO)
        {
            if (participantDTO == null) throw new NullReferenceException("Can't map, DALRowParticipantDTO is null");
            if (participantDTO.AppUserId == null) throw new NullReferenceException("Can't map, DALRowParticipantDTO AppUserId property is null");
            return new BLLAppUserDTO()
            {
                Id = participantDTO.AppUserId.Value,
                Nickname = participantDTO.Name
            };
        }

        public static BLLAppUserDTO FromDAL(DALAppUserDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, DALAppUserDTO is null");
            return new BLLAppUserDTO()
            {
                Id = dto.Id,
                Nickname = dto.UserNickname
            };
        }
    }
}