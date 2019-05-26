using Enums;

namespace DAL.App.DTO
{
    public class DALLoanGivenDTO
    {
        public string LoanTakerName { get; set; }
        public int LoanId { get; set; }
        public decimal OwedAmount { get; set; }
        public int ReceiptId { get; set; }
        public LoanStatus Status { get; set; }
    }
}