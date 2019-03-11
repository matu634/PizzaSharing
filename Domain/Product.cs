using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain
{
    public class Product : BaseEntity
    {
        [MaxLength(100)]
        [MinLength(1)]
        [Required]
        public string ProductName { get; set; }
        
        public List<ProductInCategory> ProductInCategories { get; set; }

        public List<Price> Prices { get; set; }

        public List<ReceiptRow> ReceiptRows { get; set; }

        public decimal GetPriceAtTime(DateTime dateTime)
        {
            Price currentPrice = Prices
                .FirstOrDefault(p => p.ValidTo.Ticks > dateTime.Ticks && p.ValidFrom.Ticks < dateTime.Ticks);
            if (currentPrice == null) throw new Exception($"Price for Product {ProductName}(id: {Id}) at {dateTime} was not found!");
            return currentPrice.Value;
        }
    }
}