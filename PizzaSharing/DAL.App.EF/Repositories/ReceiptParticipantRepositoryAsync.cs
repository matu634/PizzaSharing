using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ReceiptParticipantRepositoryAsync : BaseRepositoryAsync<ReceiptParticipant>, IReceiptParticipantRepository
    {
        public ReceiptParticipantRepositoryAsync(IDataContext dataContext) : base(dataContext)
        {
        }

        public override async Task<IEnumerable<ReceiptParticipant>> AllAsync()
        {
            return await RepoDbSet
                .Include(participant => participant.AppUser)
                .Include(participant => participant.Receipt)
                .ToListAsync();
        }

        public override async Task<ReceiptParticipant> FindAsync(params object[] id)
        {
            var participant = await RepoDbSet.FindAsync(id);

            if (participant != null)
            {
                await RepoDbContext.Entry(participant).Reference(p => p.AppUser).LoadAsync();
                await RepoDbContext.Entry(participant).Reference(p => p.Receipt).LoadAsync();
            }

            return participant;
        }

        public Task<ReceiptParticipant> FindOrAddAsync(int receiptId, int participantAppUserId)
        {
            var participant = RepoDbSet
                .FirstAsync(obj => obj.AppUserId == participantAppUserId && obj.ReceiptId == receiptId);
            if (participant != null) return participant;
            return null;
        }
    }
}