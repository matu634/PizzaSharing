using System;
using DAL.App.DTO;
using Domain;

namespace DAL.App.EF.Mappers
{
    public class LoanRowMapper
    {
        /// <summary>
        /// Maps Id, ReceiptRowId, LoanDTO -> ParticipantDTO -> ReceiptDTO
        /// </summary>
        /// <param name="loanRow"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static DALLoanRowDTO FromDomain(LoanRow loanRow)
        {
            if (loanRow == null) throw new NullReferenceException("Can't map, loanRow entity is null");
            
            return new DALLoanRowDTO()
            {
                Id = loanRow.Id,
                ReceiptRowId = loanRow.ReceiptRowId,
                Loan = LoanMapper.FromDomain(loanRow.Loan)
            };
        }
    }
}