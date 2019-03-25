using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class ReceiptViewModel
    {
        public Receipt Receipt { get; set; }
        public SelectList AppUsers { get; set; }
    }
}