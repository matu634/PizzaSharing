using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.masirg.Contracts.BLL.Base.Services;
using PublicApi.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IReceiptsService : IBaseService
    {
        Task<BLLReceiptDTO> GetReceiptAndRelatedData(int receiptId, int currentUserId);
        
        Task<int> NewReceipt(int userId);

        Task<bool> RemoveReceipt(int receiptId, int currentUserId);

        Task<BLLReceiptRowDTO> AddRow(BLLReceiptRowDTO receiptRowDTO, int currentUserId);

        Task<BLLReceiptRowDTO> UpdateRowAmount(BLLReceiptRowDTO dto, int userId);

        Task<bool> RemoveRow(int rowId, int userId);

        Task<BLLReceiptRowDTO> AddRowChange(int rowId, int changeId, int userId);

        Task<BLLReceiptRowDTO> AddRowParticipantAsync(BLLRowParticipantDTO newParticipant, int currentUserId);

        Task<BLLReceiptRowDTO> RemoveRowChangeAsync(int rowId, int componentId, int userId);

        Task<BLLReceiptRowDTO> RemoveRowParticipantAsync(int loanRowId, int userId);

        ReceiptRowAllDTO EditRowParticipantInvolvement();

        ReceiptRowAllDTO AddRowDiscount();

        ReceiptRowAllDTO RemoveRowDiscount();
        
        Task<List<BLLAppUserDTO>> GetAvailableRowParticipants(int rowId, int userId);
        
        Task<bool> SubmitReceipt(int receiptId, int userId);
    }
}