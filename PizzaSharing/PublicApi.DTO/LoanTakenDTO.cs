namespace PublicApi.DTO
{
    public class LoanTakenDTO
    {
        public int LoanId { get; set; }
        public decimal OwedAmount { get; set; }
        public string LoanGiverName { get; set; }
    }
}