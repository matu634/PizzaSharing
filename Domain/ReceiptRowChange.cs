namespace Domain
{
    public class ReceiptRowChange : BaseEntity
    {
        public int ReceiptRowId { get; set; }
        public ReceiptRow ReceiptRow { get; set; }

        public int ChangeId { get; set; }
        public Change Change { get; set; }
        
    }
}