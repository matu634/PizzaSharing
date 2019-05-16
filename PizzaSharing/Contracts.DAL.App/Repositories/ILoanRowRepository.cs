using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.masirg.Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ILoanRowRepository : IBaseRepositoryAsync<LoanRow>
    {
        Task<List<LoanRow>> FindByReceiptRow(int receiptRowId);
        
        Task AddAsync(int loanId, int receiptRowId, decimal involvement);
    }
}