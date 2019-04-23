using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.Product
{
    public class CreateProductViewModel
    {
        [Required]
        public int OrganizationId { get; set; }
        
        public string OrganizationName { get; set; }
        public List<SelectListItem> Categories { get; set; }

        [MinLength(1)]
        [MaxLength(64)]
        [Required(ErrorMessage = "Please enter a product name")]
        public string ProductName { get; set; }
        
        [Range(0, 10000)]
        [Required(ErrorMessage = "Please enter a valid price")]
        public decimal Price { get; set; }
        
        [Display(Name = "Categories")]
        [Required(ErrorMessage = "Please select at least one category")]
        public IEnumerable<int> SelectedCategories { get; set; }
    }
}