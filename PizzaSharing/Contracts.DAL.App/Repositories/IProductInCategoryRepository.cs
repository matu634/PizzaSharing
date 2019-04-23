using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IProductInCategoryRepository : IBaseRepositoryAsync<ProductInCategory>
    {
        Task<List<int>> CategoryIdsAsync(int productId);
        
        Task AddAsync(int productId, int categoryId);
    }
}