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

        [MinLength(1, ErrorMessageResourceName = "TooShortProductName", ErrorMessageResourceType = typeof(Resources.Product.Create))]
        [MaxLength(64, ErrorMessageResourceName = "TooLongProductName", ErrorMessageResourceType = typeof(Resources.Product.Create))]
        [Display(Name = "ProductName", ResourceType = typeof(Resources.Product.Create))]
        [Required(ErrorMessageResourceName = "MissingProductName", ErrorMessageResourceType = typeof(Resources.Product.Create))]
        public string ProductName { get; set; }
        
        //TODO: product description
        
        [Range(0, 10000, ErrorMessageResourceName = "PriceRangeValidation", ErrorMessageResourceType = typeof(Resources.Product.Create))]
        [Display(Name = "ProductPrice", ResourceType = typeof(Resources.Product.Create))]
        [Required(ErrorMessageResourceName = "MissingProductPrice", ErrorMessageResourceType = typeof(Resources.Product.Create))]
        public decimal Price { get; set; }
        
        [Display(Name = "ProductCategories" , ResourceType = typeof(Resources.Product.Create))]
        [Required(ErrorMessageResourceName = "CategoryNotSelected", ErrorMessageResourceType = typeof(Resources.Product.Create))]
        public IEnumerable<int> SelectedCategories { get; set; }
    }
}