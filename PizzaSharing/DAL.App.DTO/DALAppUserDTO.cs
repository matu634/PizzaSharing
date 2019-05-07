using System.Collections.Generic;

namespace DAL.App.DTO
{
    public class DALAppUserDTO
    {
        public int Id { get; set; }
        public string UserNickname { get; set; }

        public List<DALReceiptParticipantDTO> ReceiptParticipants { get; set; }

        public List<DALLoanDTO> GivenLoans { get; set; }
        public List<DALLoanDTO> TakenLoans { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}