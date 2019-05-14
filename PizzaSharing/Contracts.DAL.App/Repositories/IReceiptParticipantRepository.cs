using System.Threading.Tasks;
using ee.itcollege.masirg.Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IReceiptParticipantRepository : IBaseRepositoryAsync<ReceiptParticipant>
    {
        Task<ReceiptParticipant> FindOrAddAsync(int receiptId, int loanTakerId);
    }
}