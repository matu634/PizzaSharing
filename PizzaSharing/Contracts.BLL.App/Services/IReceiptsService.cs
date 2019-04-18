using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using PublicApi.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IReceiptsService : IBaseService
    {
        Task<ReceiptAllDTO> GetReceiptAndRelatedData(int receiptId, int currentUserId);
        
        Task<int> NewReceipt(int userId);

        Task<ReceiptRowAllDTO> AddRow(ReceiptRowMinDTO receiptRowDTO, int currentUserId);

        ReceiptRowAllDTO UpdateRowAmount();

        bool DeleteRow();

        ReceiptRowAllDTO AddRowChange();

        ReceiptRowAllDTO AddRowParticipant();

        ReceiptRowAllDTO RemoveRowChange();

        ReceiptRowAllDTO RemoveRowParticipant();

        ReceiptRowAllDTO EditRowParticipantInvolvement();

        bool SetReceiptFinalized();
    }
}