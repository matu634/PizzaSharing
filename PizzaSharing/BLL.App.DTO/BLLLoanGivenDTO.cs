using Enums;

namespace BLL.App.DTO
{
    public class BLLLoanGivenDTO
    {
        public int LoanId { get; set; }
        public decimal OwedAmount { get; set; }
        public string LoanTakerName { get; set; }
        public int ReceiptId { get; set; }
        public LoanStatus Status { get; set; }
    }
}