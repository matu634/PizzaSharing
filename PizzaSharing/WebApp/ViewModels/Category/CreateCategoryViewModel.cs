using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.Category
{
    public class CreateCategoryViewModel
    {
        [Required] public int OrganizationId { get; set; }

        public string OrganizationName { get; set; }

        [MinLength(1)]
        [MaxLength(64)]
        [Required(ErrorMessage = "Please enter a category name")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
    }
}