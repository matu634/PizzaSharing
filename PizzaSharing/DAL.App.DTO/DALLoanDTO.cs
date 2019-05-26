using Enums;

namespace DAL.App.DTO
{
    public class DALLoanDTO
    {
        public int Id { get; set; }
        
        public DALReceiptParticipantDTO  ReceiptParticipant { get; set; }

        public LoanStatus Status { get; set; }

        public DALAppUserDTO LoanGiver { get; set; }

        public DALAppUserDTO LoanTaker { get; set; }

        public DALLoanRowDTO LoanRows { get; set; }
        public int LoanGiverId { get; set; }
        public int LoanTakerId { get; set; }
    }
}