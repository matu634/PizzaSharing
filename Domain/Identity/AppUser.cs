using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppUser : IdentityUser<int>, IBaseEntity
    {
        public string UserNickname { get; set; }
        
        public List<Receipt> ManagedReceipts { get; set; }

        public List<ReceiptParticipant> ReceiptParticipants { get; set; }

        public List<Loan> GivenLoans { get; set; }
        
        public List<Loan> TakenLoans { get; set; }

        [MaxLength(64)]
        [MinLength(1)]
        public string FirstName { get; set; }
        
        [MaxLength(64)]
        [MinLength(1)]
        public string LastName { get; set; }

        public string FirstLastName => FirstName + " " + LastName;
    }
}