namespace DAL.App.DTO
{
    public class DALReceiptParticipantDTO
    {
        public int Id { get; set; }
        public DALAppUserDTO Participant { get; set; }
        public DALReceiptDTO Receipt { get; set; }
        public DALLoanDTO Loan { get; set; }
    }
}