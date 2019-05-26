using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.masirg.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using Domain;
using Enums;
using PublicApi.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ILoanRepository : IBaseRepositoryAsync<Loan>
    {
        Task<List<DALLoanTakenDTO>> AllUserTakenLoans(int appUserId);
        Task<List<DALLoanGivenDTO>> AllUserGivenLoans(int appUserId);
        Task<int> FindOrAddAsync(DALReceiptParticipantDTO participant, int receiptManagerId);
        
        Task<DALLoanDTO> FindAsync(int loanId);
        
        Task ChangeStatusAsync(int loanId, LoanStatus newStatus);
    }
}