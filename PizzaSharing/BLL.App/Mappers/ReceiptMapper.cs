using System;
using System.Collections.Generic;
using System.Linq;
using BLL.App.DTO;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public static class ReceiptMapper
    {
        /// <summary>
        /// Maps Id, Time, IsFinalized, Cost, ManagerId
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
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

        /// <summary>
        /// Maps Id, Time, IsFinalized, Cost, ManagerId, Rows(LOTS)
        /// </summary>
        /// <param name="receipt"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static BLLReceiptDTO FromDAL2(DALReceiptDTO receipt, List<DALReceiptRowDTO> rows)
        {
            if (receipt == null) throw new NullReferenceException("Can't map, DALReceiptDTO is null");
            var cost = decimal.Zero;
            if (rows != null && rows.Any())
            {
                cost = rows.Select(dto => dto.CurrentCost ?? decimal.Zero).Sum();
            }

            var result = new BLLReceiptDTO()
            {
                ReceiptId = receipt.ReceiptId,
                ManagerNickname = receipt.ManagerNickname,
                CreatedTime = receipt.CreatedTime,
                IsFinalized = receipt.IsFinalized,
                ReceiptRows = rows
                    .Select(ReceiptRowMapper.FromDAL)
                    .ToList(),
                SumCost = cost
            };
            return result;
        }

        /// <summary>
        /// Creates new receipt with ManagerId, CreatedTime == now, IsFinalized == false
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DALReceiptDTO FromBLL(int userId)
        {
            return new DALReceiptDTO()
            {
                ReceiptManagerId = userId,
                CreatedTime = DateTime.Now,
                IsFinalized = false
            };
        }
    }
}