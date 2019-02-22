using System;
using System.Collections.Generic;
using Domain.Identity;

namespace Domain
{
    public class Receipt
    {
        public int ReceiptId { get; set; }
        
        public List<ReceiptRow> ReceiptRows { get; set; }

        public bool IsFinalized { get; set; } = false;

        public DateTime CreatedTime { get; set; }

        public int ReceiptManagerId { get; set; }
        public AppUser ReceiptManager { get; set; }

        public List<ReceiptParticipant> ReceiptParticipants { get; set; }
        
    }
}