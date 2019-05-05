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

        public int ProductNameId { get; set; }
        public MultiLangString ProductName { get; set; }

        public List<ProductInCategory> ProductInCategories { get; set; }

        public List<Price> Prices { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public List<ReceiptRow> ReceiptRows { get; set; }

        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public string ProductAndOwnerName =>
            $"{ProductName} ({Organization?.OrganizationName ?? "Error! Product's Organization not loaded"})";
    }
}