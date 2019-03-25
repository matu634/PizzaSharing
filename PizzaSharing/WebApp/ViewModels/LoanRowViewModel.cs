using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class LoanRowViewModel
    {
        public LoanRow LoanRow { get; set; }
        public SelectList Loans { get; set; }
        public SelectList ReceiptRows { get; set; }
    }
}