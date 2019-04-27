using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.Change
{
    public class DeleteChangeViewModel
    {
        [Required]
        public int ChangeId { get; set; }
        
        [Display(Name = "ChangeName", ResourceType = typeof(Resources.Change.Delete))]
        public string ChangeName { get; set; }
        
        [Display(Name = "ChangePrice", ResourceType = typeof(Resources.Change.Delete))]
        public decimal Price { get; set; }
        
        [Required]
        public int OrganizationId { get; set; }
    }
}