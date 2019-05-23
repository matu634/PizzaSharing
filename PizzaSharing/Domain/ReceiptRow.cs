using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class ReceiptRow : BaseEntity
    {
        public int Amount { get; set; }
        
        //Example: 0.1 for 10% discount
        public decimal? RowDiscount { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int ReceiptId { get; set; }
        public Receipt Receipt { get; set; }

        public List<LoanRow> RowParticipantLoanRows { get; set; }

        public List<ReceiptRowChange> ReceiptRowChanges { get; set; }

        
        public decimal RowSumCost()
        {
            if (RowDiscount != null && (RowDiscount > 1.0M || RowDiscount < decimal.Zero))
            {
                throw new Exception($"Invalid discount value {RowDiscount}. Must be between 1.0 and 0.0");
            }
            
            if (Product.IsDeleted == true && Receipt.IsFinalized == false) return 0;
            var time = Receipt.IsFinalized == false ? DateTime.Now : Receipt.CreatedTime;
            
            
            Price currentPrice = Product?.Prices?
                .FirstOrDefault(p => p.ValidTo > time && p.ValidFrom < time);
            if (currentPrice == null)
            {
                Console.Write("Couldn't find price for product, retuning -1");
                return decimal.MinusOne;
            }
            
            var price = Amount * currentPrice.Value;

            foreach (var rowChange in ReceiptRowChanges)
            {
                Price changePrice = rowChange.Change?.Prices?
                    .FirstOrDefault(p => p.ValidTo > time && p.ValidFrom < time);

                if (changePrice == null)
                {
                    Console.WriteLine("Couldn't find price for receiptRow returning -1");
                    return decimal.MinusOne;
                }
                price += Amount * changePrice.Value;

            }
            return price * (1.0m - RowDiscount) ?? price;
        }
        
    }
}