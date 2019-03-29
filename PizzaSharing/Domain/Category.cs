using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Domain
{
    public class Category : BaseEntity
    {
        [MaxLength(100)]
        [MinLength(1)]
        [Required]
        public string CategoryName { get; set; }
        
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public List<Change> Changes { get; set; }

        public List<ProductInCategory> ProductsInCategory { get; set; }

//        [ValidateNever]
        //This will throw an exception without the ValidateNever attribute. This doesn't need to be validated.
        public string CategoryAndOwnerName => $"{CategoryName} ({Organization?.OrganizationName ?? "Error. Categories' Organization not loaded"})";
    }
}