using System;
using System.Collections.Generic;
using System.Linq;
using BLL.App.DTO;

namespace PublicApi.DTO.Mappers
{
    public static class ReceiptMapper
    {
        /// <summary>
        /// Maps time, finalized, receiptId, cost and managerId
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static ReceiptDTO FromBLL(BLLReceiptDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, BLLReceiptDTO is null");
            return new ReceiptDTO()
            {
                CreatedTime = dto.CreatedTime,
                IsFinalized = dto.IsFinalized,
                ReceiptId = dto.ReceiptId,
                SumCost = dto.SumCost,
                ReceiptManagerId = dto.ReceiptManagerId
            };
        }
        /// <summary>
        ///  Maps id, createdTime, finalized, sumCost, Rows()
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static ReceiptAllDTO FromBLLToAll(BLLReceiptDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, BLLReceiptDTO is null");
            return new ReceiptAllDTO()
            {
                CreatedTime = dto.CreatedTime,
                ManagerNickname = dto.ManagerNickname,
                IsFinalized = dto.IsFinalized,
                ReceiptId = dto.ReceiptId,
                SumCost = dto.SumCost,
                Rows = dto.ReceiptRows
                    .Select(ReceiptRowMapper.FromBLL)
                    .ToList()
            };
        }
    }
}