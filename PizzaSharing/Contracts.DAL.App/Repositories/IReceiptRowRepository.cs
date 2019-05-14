using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.masirg.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using Domain;
using PublicApi.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IReceiptRowRepository : IBaseRepositoryAsync<ReceiptRow>
    {
        Task<List<DALReceiptRowDTO>> AllReceiptsRows(int receiptId, DateTime time);
        
        Task<int?> AddAsync(DALReceiptRowDTO row);

        Task<DALReceiptRowDTO> FindRowAndRelatedDataAsync(int id);
        
        Task<int?> UpdateRowAmount(int receiptRowId, int newAmount, int userId);
        
        Task<bool> RemoveRowAsync(int rowId, int userId);
    }
}