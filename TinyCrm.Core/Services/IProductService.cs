using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Model;

namespace TinyCrm.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    interface IProductService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        bool AddProduct(AddProductOptions options);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        bool UpdateProduct(string productId,
            UpdateProductOptions options);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Product GetProductById(string id);
    }
}
