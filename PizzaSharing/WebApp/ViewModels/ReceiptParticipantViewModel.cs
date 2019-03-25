using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class ReceiptParticipantViewModel
    {
        public ReceiptParticipant ReceiptParticipant { get; set; }
        public SelectList Receipts { get; set; }
        public SelectList AppUsers { get; set; }
    }
}