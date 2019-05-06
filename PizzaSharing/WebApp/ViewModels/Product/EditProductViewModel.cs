using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.Product
{
    public class EditProductViewModel
    {
        [Required] 
        public int ProductId { get; set; }

        [MinLength(1, ErrorMessageResourceName = "TooShortDescriptionName", ErrorMessageResourceType = typeof(Resources.Product.Edit))]
        [MaxLength(10240, ErrorMessageResourceName = "TooLongDescriptionName", ErrorMessageResourceType = typeof(Resources.Product.Edit))]
        [Display(Name = "ProductDescription", ResourceType = typeof(Resources.Product.Edit))]
        [Required(ErrorMessageResourceName = "MissingProductDescription", ErrorMessageResourceType = typeof(Resources.Product.Edit))]
        public string Description { get; set; }
        
        [Required]
        public int OrganizationId { get; set; }
        
        public string OrganizationName { get; set; }
        public List<SelectListItem> Categories { get; set; }

        [MinLength(1, ErrorMessageResourceName = "TooShortProductName", ErrorMessageResourceType = typeof(Resources.Product.Edit))]
        [MaxLength(64, ErrorMessageResourceName = "TooLongProductName", ErrorMessageResourceType = typeof(Resources.Product.Edit))]
        [Display(Name = "ProductName", ResourceType = typeof(Resources.Product.Edit))]
        [Required(ErrorMessageResourceName = "MissingProductName", ErrorMessageResourceType = typeof(Resources.Product.Edit))]
        public string ProductName { get; set; }
        
        [Range(0, 10000, ErrorMessageResourceName = "PriceRangeValidation", ErrorMessageResourceType = typeof(Resources.Product.Edit))]
        [Display(Name = "ProductPrice", ResourceType = typeof(Resources.Product.Edit))]
        [Required(ErrorMessageResourceName = "MissingProductPrice", ErrorMessageResourceType = typeof(Resources.Product.Edit))]
        public decimal Price { get; set; }
        
        [Display(Name = "ProductCategories" , ResourceType = typeof(Resources.Product.Edit))]
        [Required(ErrorMessageResourceName = "CategoryNotSelected", ErrorMessageResourceType = typeof(Resources.Product.Edit))]
        public IEnumerable<int> SelectedCategories { get; set; }
    }
}