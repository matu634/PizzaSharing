using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.Change
{
    public class CreateChangeViewModel
    {
        [Required]
        public int OrganizationId { get; set; }
        
        public string OrganizationName { get; set; }
        public List<SelectListItem> Categories { get; set; }

        [MinLength(1)]
        [MaxLength(64)]
        [Required(ErrorMessage = "Please enter a change name")]
        [Display(Name = "Change Name")]
        public string ChangeName { get; set; }
        
        [Range(0, 10000)]
        [Display(Name = "Price")]
        [Required(ErrorMessage = "Please enter a valid price")]
        public decimal Price { get; set; }
        
        [Display(Name = "Categories")]
        [Required(ErrorMessage = "Please select at least one category")]
        public IEnumerable<int> SelectedCategories { get; set; }
    }
}