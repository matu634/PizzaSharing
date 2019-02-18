namespace Domain
{
    public class ReceiptRowChange
    {
        public int ReceiptRowChangeId { get; set; }
        
        public int ReceiptRowId { get; set; }
        public ReceiptRow ReceiptRow { get; set; }

        public int ChangeId { get; set; }
        public Change Change { get; set; }
        
    }
}