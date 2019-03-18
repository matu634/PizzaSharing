using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain;

namespace WebApp.Models
{
    public class CategoriesViewModel
    {
        [Display(Name = "Name")]
        //[Range(100,200)] // numbrite min max väärtus, ainult UI, mitte DB
        public List<Organization> Organizations { get; set; }
        public List<Category> Categories { get; set; }
    }
}