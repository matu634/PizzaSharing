using System;
using BLL.App.DTO;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public static class LoanTakenMapper
    {
        public static BLLLoanTakenDTO FromDAL(DALLoanTakenDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, DALLoanTakenDTO is null");
            return new BLLLoanTakenDTO()
            {
                OwedAmount = dto.OwedAmount,
                LoanId = dto.LoanId,
                LoanGiverName = dto.LoanGiverName
            };
        }
    }
}