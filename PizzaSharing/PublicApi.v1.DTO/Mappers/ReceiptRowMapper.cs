using System;
using System.Collections.Generic;
using BLL.App.DTO;

namespace PublicApi.DTO.Mappers
{
    public static class ReceiptRowMapper
    {
        /// <summary>
        ///  Maps Id, ReceiptId, Amount, Discount, Cost,
        ///     Product(5),
        ///     Changes(4),
        ///     Participants(5)
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
                result.Participants.Add(RowParticipantMapper.FromBLL(participantDTO));
            }

            return result;

        }

        /// <summary>
        /// Maps ReceiptRowId, Amount
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static BLLReceiptRowDTO FromAPI(ReceiptRowAmountChangeDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, ReceiptRowAmountChangeDTO is null");
            return new BLLReceiptRowDTO()
            {
                Amount = dto.NewAmount,
                ReceiptRowId = dto.ReceiptRowId
            };
        }

        /// <summary>
        /// Maps ProductId, ReceiptId, Amount, Discount
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static BLLReceiptRowDTO FromAPI2(ReceiptRowMinDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, ReceiptRowMinDTO is null");
            if (dto.Amount == null || dto.ProductId == null || dto.ReceiptId == null) return null;
            return new BLLReceiptRowDTO()
            {
                Amount = dto.Amount.Value,
                Discount = dto.Discount,
                ProductId = dto.ProductId.Value,
                ReceiptId = dto.ReceiptId.Value
            };
        }
    }
}