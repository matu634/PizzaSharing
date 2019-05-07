using System;

namespace PublicApi.DTO
{
    public class ReceiptSendDTO
    {
        public int ReceiptId { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsFinalized { get; set; }
        public decimal SumCost { get; set; }
    }
}