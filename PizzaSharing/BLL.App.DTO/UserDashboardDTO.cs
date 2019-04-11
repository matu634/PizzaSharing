using System;
using System.Collections.Generic;
using DAL.App.DTO;
using Domain;

//BLL to Controller
namespace BLL.App.DTO
{
    public class UserDashboardDTO
    {
        public List<ReceiptDTO> OpenReceipts { get; set; }
        public List<ReceiptDTO> ClosedReceipts { get; set; }

        public List<LoanGivenDTO> Loans { get; set; }
        public List<LoanTakenDTO> Debts { get; set; }
    }
}