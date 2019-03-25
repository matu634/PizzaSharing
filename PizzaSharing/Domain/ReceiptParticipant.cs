using Domain.Identity;

namespace Domain
{
    public class ReceiptParticipant : BaseEntity
    {
        public int ReceiptId { get; set; }
        public Receipt Receipt { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public Loan Loan { get; set; }

        public override string ToString()
        {
            return $"{AppUser.UserNickname} for receipt no. {ReceiptId}";
        }
    }
}