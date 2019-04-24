using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.ViewModels
{
    public class CategoryViewModel
    {
        public Domain.Category Category { get; set; }
        public SelectList Organizations { get; set; }
    }
}