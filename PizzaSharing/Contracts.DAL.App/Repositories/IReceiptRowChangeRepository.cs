using System.Threading.Tasks;
using ee.itcollege.masirg.Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IReceiptRowChangeRepository : IBaseRepositoryAsync<ReceiptRowChange>
    {
        Task AddAsync(int changeId, int receiptRowId);
        
        Task<bool> RemoveWhereAsync(int changeId, int rowId);
    }
}