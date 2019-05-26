using System;
using BLL.App.DTO;

namespace PublicApi.DTO.Mappers
{
    public static class LoansGivenMapper
    {
        public static LoanGivenDTO FromBLL(BLLLoanGivenDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, BLLLoanGivenDTO is null");
            return new LoanGivenDTO()
            {
                LoanId = dto.LoanId,
                ReceiptId = dto.ReceiptId,
                OwedAmount = dto.OwedAmount,
                LoanTakerName = dto.LoanTakerName,
                Status = dto.Status
            };
        }
    }
}