using DAL.App.DTO;
using Domain;

namespace DAL.App.EF.Mappers
{
    public static class ReceiptParticipantMapper
    {
        public static DALReceiptParticipantDTO FromDomain(ReceiptParticipant participant)
        {
            return new DALReceiptParticipantDTO()
            {
                Id = participant.Id,
                ReceiptId = participant.ReceiptId,
                ParticipantAppUserId = participant.AppUserId
            };
        }
    }
}