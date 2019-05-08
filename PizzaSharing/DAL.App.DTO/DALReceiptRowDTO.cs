using System.Collections.Generic;

namespace DAL.App.DTO
{
    public class DALReceiptRowDTO
    {
        public int? ReceiptRowId { get; set; }
        public int? ReceiptId { get; set; }

        public int? Amount { get; set; }
        public decimal? Discount { get; set; }

        public int? ProductId { get; set; }
        public DALProductDTO Product { get; set; }
        public List<DALChangeDTO> Changes { get; set; }

        public List<DALRowParticipantDTO> Participants { get; set; }
        public decimal? CurrentCost { get; set; }
    }
}