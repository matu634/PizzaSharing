using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using Domain;
using PublicApi.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IReceiptRowRepository : IBaseRepositoryAsync<ReceiptRow>
    {
        Task<List<DALReceiptRowDTO>> AllReceiptsRows(int receiptId, DateTime time);
        
        Task<ReceiptRow> AddAsync(DALReceiptRowMinDTO rowMin);

        Task<ReceiptRow> FindRowAndRelatedDataAsync(int id);
    }
}