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

        [MinLength(1, ErrorMessageResourceName = "TooShortChangeName", ErrorMessageResourceType = typeof(Resources.Change.Create))]
        [MaxLength(64, ErrorMessageResourceName = "TooLongChangeName", ErrorMessageResourceType = typeof(Resources.Change.Create))]
        [Display(Name = "ChangeName", ResourceType = typeof(Resources.Change.Create))]
        [Required(ErrorMessageResourceName = "MissingChangeName", ErrorMessageResourceType = typeof(Resources.Change.Create))]
        public string ChangeName { get; set; }
        
        [Range(0, 10000, ErrorMessageResourceName = "PriceRangeValidation", ErrorMessageResourceType = typeof(Resources.Change.Create))]
        [Display(Name = "ChangePrice", ResourceType = typeof(Resources.Change.Create))]
        [Required(ErrorMessageResourceName = "MissingChangePrice", ErrorMessageResourceType = typeof(Resources.Change.Create))]
        public decimal Price { get; set; }
        
        [Display(Name = "ChangeCategories" , ResourceType = typeof(Resources.Change.Create))]
        [Required(ErrorMessageResourceName = "CategoryNotSelected", ErrorMessageResourceType = typeof(Resources.Change.Create))]
        public IEnumerable<int> SelectedCategories { get; set; }
    }
}