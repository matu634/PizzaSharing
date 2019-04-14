using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IReceiptParticipantRepository : IBaseRepositoryAsync<ReceiptParticipant>
    {
        Task<ReceiptParticipant> FindOrAddAsync(int receiptId, int loanTakerId);
    }
}