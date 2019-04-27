using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.Dashboard
{
    public class CreateOrganizationViewModel
    {
        [MaxLength(64,ErrorMessageResourceName = "NameTooLongError", 
            ErrorMessageResourceType = typeof(Resources.Dashboard.Create))]
        [MinLength(1,ErrorMessageResourceName = "NameTooShortError", 
            ErrorMessageResourceType = typeof(Resources.Dashboard.Create))]
        [Display(Name = "OrganizationName", ResourceType = typeof(Resources.Dashboard.Create))]
        [Required(ErrorMessageResourceName = "MissingNameError", 
            ErrorMessageResourceType = typeof(Resources.Dashboard.Create))]
        public string Name { get; set; }
    }
}