using System;
using DAL.App.DTO;
using Domain;

namespace DAL.App.EF.Mappers
{
    public class LoanMapper
    {
        
        /// <summary>
        /// Maps id, status, ReceiptParticipantDTO -> ReceiptDTO
        /// </summary>
        /// <param name="loan"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static DALLoanDTO FromDomain(Loan loan)
        {
            if (loan == null) throw new NullReferenceException("Can't map, loan entity is null");
            
            return new DALLoanDTO()
            {
                ReceiptParticipant = ReceiptParticipantMapper.FromDomain2(loan.ReceiptParticipant),
                Id = loan.Id,
                Status = loan.Status
            };
        }

        /// <summary>
        /// Maps Id, Status, LoanGiverId, LoanTakerId
        /// </summary>
        /// <param name="loan"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static DALLoanDTO FromDomain2(Loan loan)
        {
            if (loan == null) throw new NullReferenceException("Can't map, loan entity is null");
            
            return new DALLoanDTO()
            {
                Id = loan.Id,
                Status = loan.Status,
                LoanGiverId = loan.LoanGiverId,
                LoanTakerId = loan.LoanTakerId
            };
        }
    }
}