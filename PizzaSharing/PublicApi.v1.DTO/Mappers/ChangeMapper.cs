using System;
using BLL.App.DTO;

namespace PublicApi.DTO.Mappers
{
    public static class ChangeMapper
    {
        /// <summary>
        /// Maps Id, Name, Price
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static ChangeDTO FromBLL(BLLChangeDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, BLLChangeDTO is null");
            return new ChangeDTO()
            {
                Name = dto.Name,
                Price = dto.CurrentPrice,
                ChangeId = dto.Id
            };
        }
    }
}