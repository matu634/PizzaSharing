using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IReceiptRowChangeRepository : IBaseRepositoryAsync<ReceiptRowChange>
    {
        Task AddAsync(ChangeDTO changeDTO);
    }
}