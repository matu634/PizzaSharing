using Domain.Identity;

namespace Domain
{
    public class ReceiptParticipant
    {
        public int ReceiptParticipantId { get; set; }

        public int ReceiptId { get; set; }
        public Receipt Receipt { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public Loan Loan { get; set; }
    }
}