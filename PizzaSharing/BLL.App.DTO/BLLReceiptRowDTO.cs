using System.Collections.Generic;

namespace BLL.App.DTO
{
    public class BLLReceiptRowDTO
    {
        public int? ReceiptRowId { get; set; }
        public int? ReceiptId { get; set; }

        public int? Amount { get; set; }
        public decimal? Discount { get; set; }

        public int? ProductId { get; set; }
        public BLLProductDTO Product { get; set; }
        public List<BLLChangeDTO> Changes { get; set; }

        public List<BLLRowParticipantDTO> Participants { get; set; }
        public decimal? CurrentCost { get; set; }
    }
}