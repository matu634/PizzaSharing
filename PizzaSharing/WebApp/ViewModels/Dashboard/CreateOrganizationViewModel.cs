using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.Dashboard
{
    public class CreateOrganizationViewModel
    {
        [MaxLength(64)]
        [MinLength(1)]
        [Display(Name = "Organization Name")]
        [Required(ErrorMessage = "Please enter a valid organization name")]
        public string Name { get; set; }
    }
}