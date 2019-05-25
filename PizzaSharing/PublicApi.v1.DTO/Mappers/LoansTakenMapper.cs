using System;
using BLL.App.DTO;

namespace PublicApi.DTO.Mappers
{
    public static class LoansTakenMapper
    {
        public static LoanTakenDTO FromBLL(BLLLoanTakenDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, BLLLoanTakenDTO is null");
            return new LoanTakenDTO()
            {
                LoanId = dto.LoanId,
                ReceiptId = dto.ReceiptId,
                OwedAmount = dto.OwedAmount,
                LoanGiverName = dto.LoanGiverName
            };
        }
    }
}