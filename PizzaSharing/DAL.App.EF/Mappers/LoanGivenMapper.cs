using System;
using System.Linq;
using DAL.App.DTO;
using Domain;

namespace DAL.App.EF.Mappers
{
    public static class LoanGivenMapper
    {
        public static DALLoanGivenDTO FromDomain(Loan loan)
        {
            if (loan == null) throw new NullReferenceException("Can't map, loan entity is null");
            if (!loan.ReceiptParticipant.Receipt.IsFinalized) return null;
            var sum = decimal.Zero;
            foreach (var loanRow in loan.LoanRows)
            {
                var rowSumCost = loanRow.ReceiptRow.RowSumCost();
                if (rowSumCost == decimal.MinusOne) continue; // skip loan row, if receipt row price could not be found
                sum += loanRow.Involvement * rowSumCost;
            }

            return new DALLoanGivenDTO()
            {
                LoanTakerName = loan.LoanTaker.UserNickname,
                LoanId = loan.Id,
                OwedAmount = sum,
                ReceiptId = loan.ReceiptParticipant.ReceiptId,
                Status = loan.Status
            };
        }
    }
}