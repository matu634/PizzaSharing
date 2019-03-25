using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class ReceiptRowViewModel
    {
        public ReceiptRow ReceiptRow { get; set; }
        public SelectList Receipts { get; set; }
        public SelectList Products { get; set; }
    }
}