using System.Threading.Tasks;
using ee.itcollege.masirg.Contracts.BLL.Base.Services;
using Enums;
using Microsoft.AspNetCore.Mvc;

namespace Contracts.BLL.App.Services
{
    public interface ILoanService : IBaseService
    {
        Task<int> ChangeLoanStatusAsync(int loanId, LoanStatus newStatus, int userId);
    }
}