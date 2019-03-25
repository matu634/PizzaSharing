using System.Collections;
using System.Collections.Generic;
using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class CategoryViewModel
    {
        public Category Category { get; set; }
        public SelectList Organizations { get; set; }
    }
}