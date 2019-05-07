using System.Collections.Generic;

namespace DAL.App.DTO
{
    public class DALReceiptRowDTO
    {
        public int Id { get; set; }
        public decimal? Discount { get; set; }

        public DALProductDTO Product { get; set; }

        public DALReceiptDTO Receipt { get; set; }

        public List<DALLoanRowDTO> RowParticipantsLoanRows { get; set; }

        public List<DALReceiptRowChangeDTO> ReceiptRowChanges { get; set; }
    }
}