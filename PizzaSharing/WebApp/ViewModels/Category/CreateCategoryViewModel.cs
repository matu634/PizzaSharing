using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.Category
{
    public class CreateCategoryViewModel
    {
        [Required] public int OrganizationId { get; set; }

        public string OrganizationName { get; set; }

        [MinLength(1, ErrorMessageResourceName = "TooShortCategoryName", ErrorMessageResourceType = typeof(Resources.Category.Create))]
        [MaxLength(64, ErrorMessageResourceName = "TooLongCategoryName", ErrorMessageResourceType = typeof(Resources.Category.Create))]
        [Display(Name = "CategoryName", ResourceType = typeof(Resources.Category.Create))]
        [Required(ErrorMessageResourceName = "MissingCategoryName", ErrorMessageResourceType = typeof(Resources.Category.Create))]
        public string CategoryName { get; set; }
    }
}