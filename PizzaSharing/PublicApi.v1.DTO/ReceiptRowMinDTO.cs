namespace PublicApi.DTO
{
    public class ReceiptRowMinDTO
    {
        public int? ReceiptId { get; set; }
        public int? ProductId { get; set; }
        public int? Amount { get; set; }
        public decimal? Discount { get; set; }
    }
}