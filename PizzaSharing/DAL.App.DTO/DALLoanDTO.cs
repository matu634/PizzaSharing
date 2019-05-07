namespace DAL.App.DTO
{
    public class DALLoanDTO
    {
        public int Id { get; set; }
        
        public DALReceiptParticipantDTO  ReceiptParticipant { get; set; }

        public bool IsPaid { get; set; }

        public DALAppUserDTO LoanGiver { get; set; }

        public DALAppUserDTO LoanTaker { get; set; }

        public DALLoanRowDTO LoanRows { get; set; }
    }
}