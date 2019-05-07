using System;
using System.Collections.Generic;

namespace DAL.App.DTO
{
    public class DALReceiptDTO
    {
        public int ReceiptId { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsFinalized { get; set; }
        public decimal SumCost { get; set; }
        public int ReceiptManagerId { get; set; }
    }
}