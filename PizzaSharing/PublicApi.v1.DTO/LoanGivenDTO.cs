namespace PublicApi.DTO
{
    public class LoanGivenDTO
    {
        public int LoanId { get; set; }
        public int ReceiptId { get; set; }
        public decimal OwedAmount { get; set; }
        public string LoanTakerName { get; set; }
    }
}