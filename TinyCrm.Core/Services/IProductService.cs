using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Model;
using System.Threading.Tasks;

namespace TinyCrm.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<bool> AddProductAsync(AddProductOptions options);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<bool> UpdateProductAsync(string productId,
            UpdateProductOptions options);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       Task<ApiResult<Product>> GetProductByIdAsync(string id);
    }
}
