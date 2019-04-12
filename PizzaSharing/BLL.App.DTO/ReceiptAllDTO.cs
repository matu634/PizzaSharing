using System;
using System.Collections.Generic;
using DAL.App.DTO;
using Domain;

namespace BLL.App.DTO
{
    public class ReceiptAllDTO
    {
        public int ReceiptId { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsFinalized { get; set; }

        public List<ReceiptRowAllDTO> Rows { get; set; }
    }
}