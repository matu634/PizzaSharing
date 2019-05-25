using System;
using BLL.App.DTO;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class LoanGivenMapper
    {
        public static BLLLoanGivenDTO FromDAL(DALLoanGivenDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, DALLoanGivenDTO is null");
            return new BLLLoanGivenDTO()
            {
                OwedAmount = dto.OwedAmount,
                LoanId = dto.LoanId,
                ReceiptId = dto.ReceiptId,
                LoanTakerName = dto.LoanTakerName
            };
        }
    }
}