using System;
using BLL.App.DTO;

namespace PublicApi.DTO.Mappers
{
    public static class ReceiptMapper
    {
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
    }
}