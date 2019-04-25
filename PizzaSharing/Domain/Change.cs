using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public List<ChangeInCategory> ChangeInCategories { get; set; }

        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public string ChangeAndOrganizationName =>
            $"{ChangeName} ({Organization?.OrganizationName ?? "Organization not loaded"})";
    }
}