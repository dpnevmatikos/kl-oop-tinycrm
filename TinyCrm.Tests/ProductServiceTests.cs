using System;
using Xunit;

using TinyCrm.Core.Data;

namespace TinyCrm.Tests
{
    public partial class ProductServiceTests
    {
        private readonly Core.Services.IProductService psvc_;

        public ProductServiceTests()
        {
            psvc_ = new Core.Services.ProductService(
                new TinyCrmDbContext());
        }

        [Fact]
        public void GetProductById_Success()
        {
            var product = psvc_.GetProductById("1111955");

            Assert.NotNull(product);
            Assert.Equal(2500.00M, product.Price);
        }

        [Fact]
        public void GetProductById_Failure_Null_ProductId()
        {
            var product = psvc_.GetProductById("     ");
            Assert.Null(product);

            product = psvc_.GetProductById(null);
            Assert.Null(product);
        }
    }
}
