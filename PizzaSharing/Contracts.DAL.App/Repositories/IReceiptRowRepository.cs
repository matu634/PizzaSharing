using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IReceiptRowRepository : IBaseRepositoryAsync<ReceiptRow>
    {
        Task<List<ReceiptRowAllDTO>> AllReceiptsRows(int receiptId, DateTime time);
        
        Task<ReceiptRow> AddAsync(ReceiptRowMinDTO rowMin);

        Task<ReceiptRow> FindRowAndRelatedDataAsync(int id);
    }
}