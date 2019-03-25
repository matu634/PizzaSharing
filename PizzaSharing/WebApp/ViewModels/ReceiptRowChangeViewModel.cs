using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class ReceiptRowChangeViewModel
    {
        public ReceiptRowChange ReceiptRowChange { get; set; }
        public SelectList Rows { get; set; }
        public SelectList Changes { get; set; }
    }
}