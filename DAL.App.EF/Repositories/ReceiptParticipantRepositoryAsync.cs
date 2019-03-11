using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ReceiptParticipantRepositoryAsync : BaseRepositoryAsync<ReceiptParticipant>, IReceiptParticipantRepository
    {
        public ReceiptParticipantRepositoryAsync(DbContext dbContext) : base(dbContext)
        {
        }
    }
}