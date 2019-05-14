using System.Threading.Tasks;
using ee.itcollege.masirg.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IPriceRepository : IBaseRepositoryAsync<Price>
    {
        Task AddAsync(DALPriceDTO priceDTO);
        
        Task EditAsync(DALPriceDTO priceDTO);
    }
}