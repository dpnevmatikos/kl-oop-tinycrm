namespace TinyCrm
{
    class Program
    {
        static void Main(string[] args)
        {
            var productService = new Services.ProductService();

            productService.AddProduct(
                new Model.Options.AddProductOptions() {
                    Id = "123",
                    Price = 13.33M,
                    ProductCategory = Model.ProductCategory.Cameras,
                    Name = "Camera 1"
                });

            productService.UpdateProduct("123",
                new Model.Options.UpdateProductOptions() {
                    Price = 22.22M
                });
        }
    }
}
