using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using PublicApi.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IReceiptsService : IBaseService
    {
        Task<ReceiptAllDTO> GetReceiptAndRelatedData(int receiptId, int currentUserId);
        
        Task<int> NewReceipt(int userId);

        Task<bool> RemoveReceipt(int receiptId, int currentUserId);

        Task<ReceiptRowAllDTO> AddRow(ReceiptRowMinDTO receiptRowDTO, int currentUserId);

        ReceiptRowAllDTO UpdateRowAmount();

        Task<bool> RemoveRow(int rowId, int userId);

        ReceiptRowAllDTO AddRowChange();

        ReceiptRowAllDTO AddRowParticipant();

        ReceiptRowAllDTO RemoveRowChange();

        ReceiptRowAllDTO RemoveRowParticipant();

        ReceiptRowAllDTO EditRowParticipantInvolvement();

        bool SetReceiptFinalized();
    }
}