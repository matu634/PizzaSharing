using System;
using System.Collections.Generic;
using BLL.App.DTO;

namespace PublicApi.DTO.Mappers
{
    public static class ReceiptRowMapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static ReceiptRowAllDTO FromBLL(BLLReceiptRowDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, BLLReceiptRowDTO is null");

            var result = new ReceiptRowAllDTO()
            {
                ReceiptId = dto.ReceiptId,
                Product = ProductMapper.FromBLL(dto.Product),
                Amount = dto.Amount,
                Discount = dto.Discount,
                CurrentCost = dto.CurrentCost,
                ReceiptRowId = dto.ReceiptRowId,
                Changes = new List<ChangeDTO>(),
                Participants = new List<RowParticipantDTO>()
            };
            foreach (var changeDto in dto.Changes)
            {
                result.Changes.Add(ChangeMapper.FromBLL(changeDto));
            }

            foreach (var participantDTO in dto.Participants)
            {
                result.Participants.Add(RowParticipantMapper.FromDAL(participantDTO));
            }

            return result;

        }
    }
}