using Domain.Identity;

namespace DAL.App.DTO
{
    public class LoanGivenDTO
    {
        public int LoanId { get; set; }
        public decimal OwedAmount { get; set; }
        public string LoanTakerName { get; set; }
    }
}