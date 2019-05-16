using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.masirg.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using Domain;
using PublicApi.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IReceiptRepository : IBaseRepositoryAsync<Receipt> 
    {
        Task<List<DALReceiptDTO>> AllUserReceipts(int userId, bool isFinalized);

        /// <summary>
        /// Return receipt and participants
        /// </summary>
        /// <param name="receiptId"></param>
        /// <returns></returns>
        Task<DALReceiptDTO> FindReceiptAsync(int receiptId);
        
        Task<int?> AddAsync(DALReceiptDTO receiptDTO);
    }
}