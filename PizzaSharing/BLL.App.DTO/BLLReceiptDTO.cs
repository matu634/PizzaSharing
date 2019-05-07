using System;

namespace BLL.App.DTO
{
    public class BLLReceiptDTO
    {
        public int ReceiptId { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsFinalized { get; set; }
        public decimal SumCost { get; set; }
        public int ReceiptManagerId { get; set; }
    }
}