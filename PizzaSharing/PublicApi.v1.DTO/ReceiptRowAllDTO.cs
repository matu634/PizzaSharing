using System.Collections.Generic;

namespace PublicApi.DTO
{
    public class ReceiptRowAllDTO
    {

        public int? ReceiptRowId { get; set; }
        public int? ReceiptId { get; set; }

        public int? Amount { get; set; }
        public decimal? Discount { get; set; }

        public ProductDTO Product { get; set; }
        public List<ChangeDTO> Changes { get; set; }

        public List<RowParticipantDTO> Participants { get; set; }
        public decimal? CurrentCost { get; set; }
    }
}