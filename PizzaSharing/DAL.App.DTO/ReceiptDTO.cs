using System;

namespace DAL.App.DTO
{
    public class ReceiptDTO
    {
        public int ReceiptId { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsFinalized { get; set; }
        public decimal SumCost { get; set; }
    }
}