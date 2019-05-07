using System;
using BLL.App.DTO;

namespace PublicApi.DTO.Mappers
{
    public static class RowParticipantMapper
    {
        /// <summary>
        /// Maps LoanId, ReceiptRowId, LoanRowId, Involvement, Name 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static RowParticipantDTO FromDAL(BLLRowParticipantDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, BLLRowParticipantDTO is null");
            return new RowParticipantDTO()
            {
                Name = dto.Name,
                LoanId = dto.LoanId,
                ReceiptRowId = dto.ReceiptRowId,
                Involvement = dto.Involvement,
                AppUserId = dto.AppUserId,
                LoanRowId = dto.LoanRowId
            };
        }
    }
}