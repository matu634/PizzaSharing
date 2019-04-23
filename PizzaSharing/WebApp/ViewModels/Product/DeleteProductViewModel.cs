using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.Product
{
    public class DeleteProductViewModel
    {
        [Required]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        
        [Required]
        public int OrganizationId { get; set; }
    }
}