using System;
using System.Linq;
using DAL.App.DTO;
using Domain;

namespace DAL.App.EF.Mappers
{
    public static class ReceiptMapper
    {
        /// <summary>
        /// Maps Id, Time, ManagerId, Cost, IsFinalized
        /// </summary>
        /// <param name="receipt"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static DALReceiptDTO FromDomain(Receipt receipt)
        {
            if (receipt == null) throw new NullReferenceException("Can't map, receipt domain entity is null");
            
            var sum = decimal.Zero;
            foreach (var row in receipt.ReceiptRows)
            {
                var rowSumCost = row.RowSumCost();
                if (rowSumCost == decimal.MinusOne)
                {
                    Console.WriteLine("Couldn't find price for a row, skipping");
                    continue;
                }
                sum += rowSumCost;
            }

            return new DALReceiptDTO()
            {
                CreatedTime = receipt.CreatedTime,
                IsFinalized = receipt.IsFinalized,
                ReceiptId = receipt.Id,
                ReceiptManagerId = receipt.ReceiptManagerId,
                SumCost = sum
            };
        }

        /// <summary>
        /// Maps Id, Time, IsFinalized, ManagerId, Participants(id, appUserId, receiptId)
        /// </summary>
        /// <param name="receipt"></param>
        /// <returns></returns>
        public static DALReceiptDTO FromDomain2(Receipt receipt)
        {
            return new DALReceiptDTO()
            {
                ReceiptId = receipt.Id,
                CreatedTime = receipt.CreatedTime,
                IsFinalized = receipt.IsFinalized,
                ReceiptManagerId = receipt.ReceiptManagerId,
                ReceiptParticipants = receipt.ReceiptParticipants
                    .Select(ReceiptParticipantMapper.FromDomain)
                    .ToList()
            };
        }
    }
}