using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using Domain;
using PublicApi.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ILoanRepository : IBaseRepositoryAsync<Loan>
    {
        Task<List<DALLoanTakenDTO>> AllUserTakenLoans(int appUserId);
        Task<List<DALLoanGivenDTO>> AllUserGivenLoans(int appUserId);
        Task<Loan> FindOrAddAsync(ReceiptParticipant participant);
    }
}