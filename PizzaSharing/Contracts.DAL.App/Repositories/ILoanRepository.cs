using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ILoanRepository : IBaseRepositoryAsync<Loan>
    {
        Task<List<LoanTakenDTO>> AllUserTakenLoans(int appUserId);
        Task<List<LoanGivenDTO>> AllUserGivenLoans(int appUserId);
        Task<Loan> FindOrAddAsync(ReceiptParticipant participant);
    }
}