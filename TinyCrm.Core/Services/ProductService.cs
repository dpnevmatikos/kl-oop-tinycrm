using System.Linq;
using System.Collections.Generic;

using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductService : IProductService
    {
        private List<Product> ProductsList = new List<Product>();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public bool AddProduct(AddProductOptions options)
        {
            if (options == null) {
                return false;
            }

            var product = GetProductById(options.Id); 

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

            product = new Product() {
                Id = options.Id,
                Name = options.Name,
                Price = options.Price,
                Category = options.ProductCategory
            };

            product.Id = options.Id;
            product.Name = options.Name;
            product.Price = options.Price;
            product.Category = options.ProductCategory;

            ProductsList.Add(product);

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public bool UpdateProduct(string productId,
            UpdateProductOptions options)
        {
            if (options == null) {
                return false;
            }

            var product = GetProductById(productId);
            if (product == null) { 
                return false; 
            }

            if (!string.IsNullOrWhiteSpace(options.Description)) {
                product.Description = options.Description;
            }

            if (options.Price != null &&
              options.Price <= 0) {
                return false;
            }

            if (options.Price != null) {
                if (options.Price <= 0) {
                    return false;
                } else {
                    product.Price = options.Price.Value;
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
        public Product GetProductById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) {
                return null;
            } 

            return ProductsList.
                SingleOrDefault(s => s.Id.Equals(id));
        }
    }
}
