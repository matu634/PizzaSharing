using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Organization
    {
        public int OrganizationId { get; set; }
        
        [MaxLength(100)]
        [MinLength(1)]
        [Required]
        public string OrganizationName { get; set; }
        
        public List<Category> Categories { get; set; }
    }
}