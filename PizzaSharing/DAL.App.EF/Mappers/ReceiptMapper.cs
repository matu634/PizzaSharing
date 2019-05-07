using System;
using DAL.App.DTO;
using Domain;

namespace DAL.App.EF.Mappers
{
    public static class ReceiptMapper
    {
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
    }
}