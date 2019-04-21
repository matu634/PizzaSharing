using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IChangeRepository : IBaseRepositoryAsync<Change>
    {
        Task<Change> FindAsync(int id);
    }
}