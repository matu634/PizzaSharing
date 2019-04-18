using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IReceiptRepository : IBaseRepositoryAsync<Receipt> 
    {
        Task<List<ReceiptDTO>> AllUserReceipts(int userId, bool isFinalized);

        Task<Receipt> AddAsync(ReceiptDTO receiptDTO);
        
        Task<Receipt> FindMinAsync(int receiptId);
    }
}