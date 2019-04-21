using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain
{
    public class Change : BaseEntity
    {
        [MaxLength(100)]
        [MinLength(1)]
        [Required]
        public string ChangeName { get; set; }

        public List<ReceiptRowChange> ReceiptRowChanges { get; set; }

        public List<Price> Prices { get; set; }

        public List<ChangeInCategory> ChangeInCategories { get; set; }

        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public string ChangeAndOrganizationName =>
            $"{ChangeName} ({Organization?.OrganizationName ?? "Organization not loaded"})";
        
        public decimal GetPriceAtTime(DateTime dateTime)
        {
            Price currentPrice = Prices
                .FirstOrDefault(p => p.ValidTo.Ticks > dateTime.Ticks && p.ValidFrom.Ticks < dateTime.Ticks);
            if (currentPrice == null)
                throw new Exception($"Price for change {ChangeName}(id:{Id}) at {dateTime} was not found!");
            return currentPrice.Value;
        }
    }
}