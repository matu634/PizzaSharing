using System;
using System.Collections.Generic;

namespace PublicApi.DTO
{
    public class ReceiptAllDTO
    {
        public int ReceiptId { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsFinalized { get; set; }

        public List<ReceiptRowAllDTO> Rows { get; set; }
        public decimal SumCost { get; set; }
    }
}