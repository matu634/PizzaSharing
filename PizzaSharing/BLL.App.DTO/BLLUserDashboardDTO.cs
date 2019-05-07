using System.Collections.Generic;

namespace BLL.App.DTO
{
    public class BLLUserDashboardDTO
    {
        public List<BLLReceiptDTO> OpenReceipts { get; set; }
        public List<BLLReceiptDTO> ClosedReceipts { get; set; }

        public List<BLLLoanGivenDTO> Loans { get; set; }
        public List<BLLLoanTakenDTO> Debts { get; set; }
    }
}