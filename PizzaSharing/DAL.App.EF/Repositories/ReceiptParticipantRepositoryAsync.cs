using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using ee.itcollege.masirg.Contracts.DAL.Base;
using ee.itcollege.masirg.DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ReceiptParticipantRepositoryAsync : BaseRepositoryAsync<ReceiptParticipant>,
        IReceiptParticipantRepository
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

        public async Task<ReceiptParticipant> FindOrAddAsync(int receiptId, int loanTakerId)
        {
            var participant = await RepoDbSet
                .FirstOrDefaultAsync(obj => obj.AppUserId == loanTakerId && obj.ReceiptId == receiptId);

            if (participant != null) return participant;

            participant = (await RepoDbSet.AddAsync(new ReceiptParticipant
            {
                AppUserId = loanTakerId,
                ReceiptId = receiptId
            })).Entity;
            await RepoDbContext.Entry(participant).Reference(p => p.Receipt).LoadAsync();
            await RepoDbContext.Entry(participant).Reference(p => p.AppUser).LoadAsync();
            return participant;
        }
    }
}