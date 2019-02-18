using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class ReceiptRow
    {
        public int ReceiptRowId { get; set; }
        
        public int Amount { get; set; }
        //Example: 0.1 for 10% discount
        public decimal RowDiscount { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int ReceiptId { get; set; }
        public Receipt Receipt { get; set; }

        public List<LoanRow> RowParticpantLoanRows { get; set; }

        public List<ReceiptRowChange> ReceiptRowChanges { get; set; }

        public decimal RowSumCost()
        {
            //TODO: look at product price(receipt datetime matches a price datetime) and add change prices on top
            Price currentPrice = Product.Prices
                .FirstOrDefault(p => p.ValidTo.Ticks > Receipt.CreatedTime.Ticks && p.ValidFrom.Ticks < Receipt.CreatedTime.Ticks);
            if (currentPrice == null) throw new Exception("Couldn't find price for product!");
            
            decimal price = Amount * currentPrice.Value;

            foreach (var rowChange in ReceiptRowChanges)
            {
                Price changePrice = rowChange.Change.Prices
                    .FirstOrDefault(p => p.ValidTo.Ticks > Receipt.CreatedTime.Ticks && p.ValidFrom.Ticks < Receipt.CreatedTime.Ticks);
                if (changePrice == null) throw new Exception("Couldn't find price for row change!");
                price += Amount * changePrice.Value;

            }
            
            
            return 1.0m;
        }
    }
}