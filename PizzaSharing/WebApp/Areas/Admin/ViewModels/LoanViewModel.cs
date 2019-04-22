using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class LoanViewModel
    {
        public Loan Loan { get; set; }
        public SelectList LoanGivers { get; set; }
        public SelectList LoanTakers { get; set; }
        public SelectList ReceiptParticipants { get; set; }
    }
}