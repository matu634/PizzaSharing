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

        /// <summary>
        /// Returns receipt row with everything
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DALReceiptRowDTO> FindRowAndRelatedDataAsync(int id);

        /// <summary>
        /// Returns receipt row with  amount, discount, productId, receiptId, rowId
        /// </summary>
        /// <param name="rowId"></param>
        /// <returns></returns>
        Task<DALReceiptRowDTO> FindAsync(int rowId);
        
        Task<int?> UpdateRowAmount(int receiptRowId, int newAmount, int userId);
        
        Task<bool> RemoveRowAsync(int rowId, int userId);
    }
}