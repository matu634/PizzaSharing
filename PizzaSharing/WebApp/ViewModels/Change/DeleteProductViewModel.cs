using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.Change
{
    public class DeleteChangeViewModel
    {
        [Required]
        public int ChangeId { get; set; }
        public string ChangeName { get; set; }
        public decimal Price { get; set; }
        
        [Required]
        public int OrganizationId { get; set; }
    }
}