using System;
using System.Linq;
using BLL.App.DTO;

namespace PublicApi.DTO.Mappers
{
    public static class UserDashboardMapper
    {
        public static UserDashboardDTO FromBLL(BLLUserDashboardDTO dto)
        {
            if (dto == null) throw new NullReferenceException("Can't map, BLLUserDashboardDTO is null");
            
            return new UserDashboardDTO()
            {
                Debts = dto.Debts.Select(LoansTakenMapper.FromBLL).ToList(),
                Loans = dto.Loans.Select(LoansGivenMapper.FromBLL).ToList(),
                ClosedReceipts = dto.ClosedReceipts.Select(ReceiptMapper.FromBLL).ToList(),
                OpenReceipts = dto.OpenReceipts.Select(ReceiptMapper.FromBLL).ToList()
            };
        }
    }
}