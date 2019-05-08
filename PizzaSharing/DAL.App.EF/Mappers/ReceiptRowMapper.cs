using System;
using System.Collections.Generic;
using DAL.App.DTO;
using DAL.App.EF.Helpers;
using Domain;
using PublicApi.DTO;

namespace DAL.App.EF.Mappers
{
    public static class ReceiptRowMapper
    {
        /// <summary>
        /// Maps Amount, Product(...), Changes(...), Participants(...), Discount, ReceiptId, RowId, Cost, 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static DALReceiptRowDTO FromDomain(ReceiptRow row, DateTime time)
        {
            if (row == null) throw new NullReferenceException("Can't map, row entity is null");

            if (row.Product?.IsDeleted ?? true) return null;
            
            var product = ProductMapper.FromDomain2(row.Product, time);
            var changes = new List<DALChangeDTO>();
            
            foreach (var rowChange in row.ReceiptRowChanges)
            {
                var changeDTO = ChangeMapper.FromDomain2(rowChange.Change, time);
                if (changeDTO == null || changeDTO.CurrentPrice == decimal.MinusOne) continue;
                changes.Add(changeDTO);
            }
            
            var participants = new List<DALRowParticipantDTO>();
            foreach (var loanRow in row.RowParticipantLoanRows)
            {
                participants.Add(new DALRowParticipantDTO()
                {
                    Name = loanRow.Loan.LoanTaker.UserNickname,
                    Involvement = loanRow.Involvement,
                    ReceiptRowId = row.Id,
                    LoanId = loanRow.LoanId,
                    AppUserId = loanRow.Loan.LoanTakerId,
                    LoanRowId = loanRow.Id
                });
            }
            
            return new DALReceiptRowDTO()
            {
                Amount = row.Amount,
                Product = product,
                Changes = changes,
                Discount = row.RowDiscount,
                ReceiptId = row.ReceiptId,
                ReceiptRowId = row.Id,
                CurrentCost = row.RowSumCost(),
                Participants = participants
            };
        }
        /// <summary>
        /// Maps Amount, ProductId, RowDiscount, ReceiptId
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ReceiptRow FromDAL(DALReceiptRowDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, DALReceiptRowDTO is null");
            if (dto.Amount == null || dto.ProductId == null || dto.ReceiptId == null) return null;
            return new ReceiptRow()
            {
                Amount = dto.Amount.Value,
                ProductId = dto.ProductId.Value,
                RowDiscount = dto.Discount,
                ReceiptId = dto.ReceiptId.Value
            };
        }
    }
}