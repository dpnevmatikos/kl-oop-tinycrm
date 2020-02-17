using System;
using Xunit;
using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Data;
using System.Threading.Tasks;

namespace TinyCrm.Tests
{
    public partial class ProductServiceTests
    {
        [Fact]
        public async Task AddProduct_Success()
        {
            var options = new AddProductOptions();
            options.Id = $"123{DateTime.Now.Millisecond}";
            options.Name = "alex";
            options.Price = 1.2M;
            options.ProductCategory = Core.Model.ProductCategory.Computers;

            var result = await psvc_.AddProductAsync(options);
            Assert.True(result);

            var p = await psvc_.GetProductByIdAsync(options.Id);
            Assert.NotNull(p);
            Assert.Equal(options.Name, p.Data.Name);
            Assert.Equal(options.Price, p.Data.Price);
            Assert.Equal(options.ProductCategory, p.Data.Category);
        }

        [Fact]
        public async Task AddProduct_Fail_InvalidCategory()
        {
            var options = new AddProductOptions();
            options.Id = $"123{DateTime.Now.Millisecond}";
            options.Name = "alex";
            options.Price = 1.2M;

            var result = await psvc_.AddProductAsync(options);
            Assert.False(result);
        }
    }
}
