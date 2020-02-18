using System;
using System.Linq;
using System.Collections.Generic;

using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TinyCrm.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly TinyCrmDbContext context;

        public ProductService(TinyCrmDbContext ctx)
        {
            context = ctx ??
                throw new ArgumentNullException(nameof(ctx));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<bool> AddProductAsync(AddProductOptions options)
        {
            if (options == null) {
                return false;
            }

            var product =await GetProductByIdAsync(options.Id); 

            if (product != null) {
                return false;
            }

            if (string.IsNullOrWhiteSpace(options.Name)) {
                return false;
            }

            if (options.Price <= 0) {
                return false;
            }

            if (options.ProductCategory ==
              ProductCategory.Invalid) {
                return false;
            }

            product.Data = new Product() {
                Id = options.Id,
                Name = options.Name,
                Price = options.Price,
                Category = options.ProductCategory
            };

            context.Add(product);

            var success = false;

            try {
                success = context.SaveChanges() > 0;
            } catch (Exception) {
                // log
            }

            return success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<bool> UpdateProductAsync(string productId,
            UpdateProductOptions options)
        {
            if (options == null) {
                return false;
            }

            var product = await GetProductByIdAsync(productId);
            if (product == null) { 
                return false; 
            }

            if (!string.IsNullOrWhiteSpace(options.Description)) {
                product.Data.Description = options.Description;
            }

            if (options.Price != null &&
              options.Price <= 0) {
                return false;
            }

            if (options.Price != null) {
                if (options.Price <= 0) {
                    return false;
                } else {
                    product.Data.Price = options.Price.Value;
                }
            }

            if (options.Discount != null &&
              options.Discount < 0) {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResult<Product>> GetProductByIdAsync(
            string id)
        {
            if (string.IsNullOrWhiteSpace(id)) {
                return new ApiResult<Product>(
                    StatusCode.BadRequest, "null id");
            }

            var product =  await context
                .Set<Product>()
                .SingleOrDefaultAsync(s => s.Id == id);

            if (product == null) {
                return new ApiResult<Product>(
                    StatusCode.NotFound, "product not found ");
            }

            var api = new ApiResult<Product>();
            api.Data = product;
            api.ErrorCode = StatusCode.Ok;

            return api;
        }
    }
}
