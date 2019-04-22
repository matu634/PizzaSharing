using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace DAL.App.EF.Helpers
{
    public static class PriceFinder
    {
        public static decimal? ForProduct(Product product, IEnumerable<Price> prices, DateTime time)
        {
            if (prices == null) return null;
            
            var currentPrice = prices.FirstOrDefault(p => p.ValidTo.Ticks > time.Ticks && p.ValidFrom.Ticks < time.Ticks);
            
            if (currentPrice == null) return null;
            return currentPrice.Value;
        }
        
        public static decimal? ForChange(Change change, IEnumerable<Price> prices, DateTime time)
        {
            if (prices == null) return null;
            
            var currentPrice = prices.FirstOrDefault(c => c.ValidTo.Ticks > time.Ticks && c.ValidFrom.Ticks < time.Ticks);
            
            if (currentPrice == null) return null;
            return currentPrice.Value;
        }
    }
}