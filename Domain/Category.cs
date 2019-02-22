using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Category
    {
        public int CategoryId { get; set; }
        
        [MaxLength(100)]
        [MinLength(1)]
        [Required]
        public string CategoryName { get; set; }
        
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public List<Change> Changes { get; set; }

        public List<ProductInCategory> ProductsInCategory { get; set; }
    }
}