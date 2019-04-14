using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface ILoanRepository : IBaseRepositoryAsync<Loan>
    {
        Task<List<LoanTakenDTO>> AllUserTakenLoans(int appUserId);
        Task<List<LoanGivenDTO>> AllUserGivenLoans(int appUserId);
        Task<Loan> FindOrAddAsync(ReceiptParticipant participant);
    }
}