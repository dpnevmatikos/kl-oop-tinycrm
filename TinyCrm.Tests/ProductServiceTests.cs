using System;

using Xunit;
using Autofac;

using TinyCrm.Core.Services;
using System.Threading.Tasks;
using TinyCrm.Core;

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
        public async Task GetProductById_Success()
        {
            var product =await psvc_.GetProductByIdAsync("1111955");

            Assert.Equal(StatusCode.Ok, product.ErrorCode);
            
        }

        [Fact]
        public async Task GetProductById_Failure_Null_ProductId()
        {
            var product = await psvc_.GetProductByIdAsync("     ");
            Assert.Null(product);

            product = await psvc_.GetProductByIdAsync(null);
            Assert.Null(product);
        }
    }
}
