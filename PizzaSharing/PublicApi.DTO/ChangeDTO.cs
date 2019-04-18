namespace PublicApi.DTO
{
    public class ChangeDTO
    {
        public int? ChangeId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int? ReceiptRowId { get; set; }
        public int? OrganizationId { get; set; }
        public int? CategoryId { get; set; }
    }
}