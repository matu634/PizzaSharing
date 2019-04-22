using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Domain
{
    public class Product : BaseEntity
    {
        [MaxLength(100)]
        [MinLength(1)]
        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        public List<ProductInCategory> ProductInCategories { get; set; }

        public List<Price> Prices { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public List<ReceiptRow> ReceiptRows { get; set; }

        /*
        public decimal GetPriceAtTime(DateTime dateTime)
        {
            if (Prices == null) return -1;
            Price currentPrice = Prices
                .FirstOrDefault(p => p.ValidTo.Ticks > dateTime.Ticks && p.ValidFrom.Ticks < dateTime.Ticks);
            if (currentPrice == null)
                throw new Exception($"Price for Product {ProductName}(id: {Id}) at {dateTime} was not found!");
            return currentPrice.Value;
        }
        */

        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public string ProductAndOwnerName =>
            $"{ProductName} ({Organization?.OrganizationName ?? "Error! Product's Organization not loaded"})";
    }
}