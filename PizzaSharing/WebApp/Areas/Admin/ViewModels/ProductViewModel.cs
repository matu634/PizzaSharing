using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public SelectList Organizations { get; set; }
    }
}