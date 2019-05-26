using Enums;

namespace DAL.App.DTO
{
    public class DALLoanTakenDTO
    {
        public int LoanId { get; set; }
        public decimal OwedAmount { get; set; }
        public string LoanGiverName { get; set; }
        public int ReceiptId { get; set; }
        public LoanStatus Status { get; set; }
    }
}