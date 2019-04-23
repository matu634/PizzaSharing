using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface IProductService : IBaseService
    {
        Task<bool> AddProductAsync(BLLProductDTO productDTO);
        
        Task<BLLProductDTO> GetProductAsync(int productId);
        
        Task<bool> DeleteProductAsync(int productId);
        
        Task<bool> EditProduct(BLLProductDTO productDTO);
    }
}