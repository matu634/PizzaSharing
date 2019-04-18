using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IReceiptRowChangeRepository : IBaseRepositoryAsync<ReceiptRowChange>
    {
        Task AddAsync(ChangeDTO changeDTO);
    }
}