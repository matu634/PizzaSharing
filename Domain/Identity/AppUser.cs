using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public string UserNickname { get; set; }
        
        public List<Receipt> ManagedReceipts { get; set; }

        public List<ReceiptParticipant> ReceiptParticipants { get; set; }

        public List<Loan> GivenLoans { get; set; }
        
        public List<Loan> TakenLoans { get; set; }
    }
}