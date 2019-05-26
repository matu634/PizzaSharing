using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Identity;
using Enums;

namespace Domain
{
    public class Loan : BaseEntity
    {
        public int ReceiptParticipantId { get; set; }
        public ReceiptParticipant ReceiptParticipant { get; set; }

        [Required]
        public LoanStatus Status { get; set; }

        public int LoanGiverId { get; set; }
        public AppUser LoanGiver { get; set; }

        public int LoanTakerId { get; set; }
        public AppUser LoanTaker { get; set; }

        public List<LoanRow> LoanRows { get; set; }
    }
}