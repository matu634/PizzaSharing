using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.Product
{
    public class DeleteProductViewModel
    {
        [Required]
        public int ProductId { get; set; }
        
        [Display(Name = "ProductName", ResourceType = typeof(Resources.Product.Delete))]
        public string ProductName { get; set; }
        
        [Display(Name = "ProductDescription", ResourceType = typeof(Resources.Product.Delete))]
        public string Description { get; set; }
        
        [Display(Name = "ProductPrice", ResourceType = typeof(Resources.Product.Delete))]
        public decimal Price { get; set; }
        
        [Required]
        public int OrganizationId { get; set; }
    }
}