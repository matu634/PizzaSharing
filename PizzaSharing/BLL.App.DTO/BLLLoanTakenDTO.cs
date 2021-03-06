using Enums;

namespace BLL.App.DTO
{
    public class BLLLoanTakenDTO
    {
        public int LoanId { get; set; }
        public decimal OwedAmount { get; set; }
        public string LoanGiverName { get; set; }
        public int ReceiptId { get; set; }
        public LoanStatus Status { get; set; }
    }
}