using System;
using DAL.App.DTO;
using Domain;

namespace DAL.App.EF.Mappers
{
    public static class ReceiptParticipantMapper
    {
        /// <summary>
        /// Maps Id, ReceiptId, ParticipantAppUserId
        /// </summary>
        /// <param name="participant"></param>
        /// <returns></returns>
        public static DALReceiptParticipantDTO FromDomain(ReceiptParticipant participant)
        {
            return new DALReceiptParticipantDTO()
            {
                Id = participant.Id,
                ReceiptId = participant.ReceiptId,
                ParticipantAppUserId = participant.AppUserId
            };
        }
        
        /// <summary>
        /// Maps Id, ReceiptId, Receipt, ParticipantAppUserId
        /// </summary>
        /// <param name="participant"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static DALReceiptParticipantDTO FromDomain2(ReceiptParticipant participant)
        {
            if (participant == null) throw new NullReferenceException("Can't map, loan entity is null");
            return new DALReceiptParticipantDTO()
            {
                Id = participant.Id,
                ReceiptId = participant.ReceiptId,
                ParticipantAppUserId = participant.AppUserId,
                Receipt = ReceiptMapper.FromDomain3(participant.Receipt)
            };
        }
    }
}