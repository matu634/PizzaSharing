namespace DAL.App.DTO
{
    public class DALLoanRowDTO
    {
        public int Id { get; set; }
        public DALLoanDTO Loan { get; set; }
        public DALReceiptRowDTO ReceiptRow { get; set; }
        public decimal Involvement { get; set; }
        
        public int ReceiptRowId { get; set; }
    }
}