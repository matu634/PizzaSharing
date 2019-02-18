using System;

namespace Domain
{
    public class Price
    {
        public int PriceId { get; set; }
        
        public decimal Value { get; set; }
        
        public int? ProductId { get; set; }
        public Product Product { get; set; }

        
        public int? ChangeId { get; set; }
        public Change Change { get; set; }

        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}