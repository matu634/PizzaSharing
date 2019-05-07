using System;
using BLL.App.DTO;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public static class ReceiptMapper
    {
        public static BLLReceiptDTO FromDAL(DALReceiptDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, DALReceiptDTO is null");
            
            return new BLLReceiptDTO()
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