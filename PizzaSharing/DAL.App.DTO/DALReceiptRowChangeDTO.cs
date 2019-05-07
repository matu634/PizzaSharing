namespace DAL.App.DTO
{
    public class DALReceiptRowChangeDTO
    {
        public int Id { get; set; }
        public DALChangeDTO Change { get; set; }
        public DALReceiptRowDTO ReceiptRow { get; set; }
    }
}