using System;

using Xunit;
using Autofac;

using TinyCrm.Core.Services;

namespace TinyCrm.Tests
{
    public partial class ProductServiceTests : IClassFixture<TinyCrmFixture>
    {
        private readonly IProductService psvc_;

        public ProductServiceTests(TinyCrmFixture fixture)
        {
            psvc_ = fixture.Container.Resolve<IProductService>();
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
