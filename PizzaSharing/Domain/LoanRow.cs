namespace Domain
{
    public class LoanRow : BaseEntity
    {
        public bool IsPaid { get; set; }

        public int LoanId { get; set; }
        public Loan Loan { get; set; }

        public int ReceiptRowId { get; set; }
        public ReceiptRow ReceiptRow { get; set; }

        public decimal Involvement { get; set; }
    }
}