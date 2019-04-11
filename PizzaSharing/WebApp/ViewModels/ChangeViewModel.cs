using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class ChangeViewModel
    {
        public Change Change { get; set; }
        public SelectList Categories { get; set; }
        public SelectList Organizations { get; set; }
    }
}