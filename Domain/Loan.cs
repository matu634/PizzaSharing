using System.Collections.Generic;
using Domain.Identity;

namespace Domain
{
    public class Loan
    {
        public int LoanId { get; set; }

        public int ReceiptParticipantId { get; set; }
        public ReceiptParticipant ReceiptParticipant { get; set; }

        public bool IsPaid { get; set; }

        public int LoanGiverId { get; set; }
        public AppUser LoanGiver { get; set; }

        public int LoanTakerId { get; set; }
        public AppUser LoanTaker { get; set; }

        public List<LoanRow> LoanRows { get; set; }
    }
}