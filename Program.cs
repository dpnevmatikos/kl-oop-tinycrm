using TinyCrm.Core.Services;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm
{
    class Program
    {
        static void Main(string[] args)
        {
            var productService = new ProductService();

            productService.AddProduct(
                new AddProductOptions() {
                    Id = "123",
                    Price = 13.33M,
                    ProductCategory = ProductCategory.Cameras,
                    Name = "Camera 1"
                });

            productService.UpdateProduct("123",
                new UpdateProductOptions() {
                    Price = 22.22M
                });
        }
    }
}
