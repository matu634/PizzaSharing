using System.Threading.Tasks;
using DAL.App.DTO;
using ee.itcollege.masirg.Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IReceiptParticipantRepository : IBaseRepositoryAsync<ReceiptParticipant>
    {
        Task<DALReceiptParticipantDTO> FindOrAddAsync(int receiptId, int loanTakerId);
    }
}