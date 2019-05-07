using System;
using BLL.App.DTO;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public static class RowParticipantMapper
    {
        /// <summary>
        /// Maps LoanId, ReceiptRowId, LoanRowId, Involvement, Name 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static BLLRowParticipantDTO FromDAL(DALRowParticipantDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, DALRowParticipantDTO is null");
            return new BLLRowParticipantDTO()
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