using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class ProductInCategoryViewModel
    {
        public ProductInCategory ProductInCategory { get; set; }
        public SelectList Products { get; set; }
        public SelectList Categories { get; set; }
    }
}