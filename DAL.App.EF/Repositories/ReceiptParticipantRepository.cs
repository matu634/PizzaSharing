using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ReceiptParticipantRepository : BaseRepository<ReceiptParticipant>, IReceiptParticipantRepository
    {
        public ReceiptParticipantRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}