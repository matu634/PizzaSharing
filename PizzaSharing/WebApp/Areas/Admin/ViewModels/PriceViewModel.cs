using System.Collections.Generic;
using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class PriceViewModel
    {
        public Price Price { get; set; }
        public IEnumerable<SelectListItem> ChangeSelectList { get; set; }
        public IEnumerable<SelectListItem> ProductSelectList { get; set; }
    }
}