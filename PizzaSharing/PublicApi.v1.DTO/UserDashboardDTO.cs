using System.Collections.Generic;

//BLL to Controller
namespace PublicApi.DTO
{
    public class UserDashboardDTO
    {
        public List<ReceiptDTO> OpenReceipts { get; set; }
        public List<ReceiptDTO> ClosedReceipts { get; set; }

        public List<LoanGivenDTO> Loans { get; set; }
        public List<LoanTakenDTO> Debts { get; set; }
    }
}