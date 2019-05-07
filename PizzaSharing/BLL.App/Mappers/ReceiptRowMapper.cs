using System;
using System.Collections.Generic;
using BLL.App.DTO;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public static class ReceiptRowMapper
    {
        
        /// <summary>
        /// Maps Id, ReceiptId, Amount, Discount, Cost,
        ///     Product(Id, Price, Name, Description, OrganizationId),
        ///     Changes(id, name, price, orgId),
        ///     Participants(Maps LoanId, ReceiptRowId, LoanRowId, Involvement, Name)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static BLLReceiptRowDTO FromDAL(DALReceiptRowDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, DALReceiptRowDTO is null");
            var result = new BLLReceiptRowDTO
            {
                ReceiptId = dto.ReceiptId,
                Product = ProductMapper.FromDAL2(dto.Product),
                Amount = dto.Amount,
                Discount = dto.Discount,
                CurrentCost = dto.CurrentCost,
                ReceiptRowId = dto.ReceiptRowId,
                Changes = new List<BLLChangeDTO>(),
                Participants = new List<BLLRowParticipantDTO>()
            };
            foreach (var changeDto in dto.Changes)
            {
                result.Changes.Add(ChangeMapper.FromDAL2(changeDto));
            }

            foreach (var participantDTO in dto.Participants)
            {
                result.Participants.Add(RowParticipantMapper.FromDAL(participantDTO));
            }

            return result;

        }
    }

    
}