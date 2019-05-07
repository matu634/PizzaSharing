namespace DAL.App.DTO
{
    public class DALRowParticipantDTO
    {
        public int LoanRowId { get; set; }
        public int ReceiptRowId { get; set; }
        public int LoanId { get; set; }

        public string Name { get; set; }

        public int? AppUserId { get; set; }
        public decimal? Involvement { get; set; }
    }
}