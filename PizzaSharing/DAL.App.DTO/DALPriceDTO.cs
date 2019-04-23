using System;

namespace DAL.App.DTO
{
    public class DALPriceDTO
    {
        public decimal Value { get; set; }
        public int? ProductId { get; set; }
        public int? ChangeId { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}