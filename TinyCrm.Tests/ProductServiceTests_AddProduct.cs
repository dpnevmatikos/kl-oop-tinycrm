using System;
using Xunit;
using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Data;

namespace TinyCrm.Tests
{
    public partial class ProductServiceTests
    {
        [Fact]
        public void AddProduct_Success()
        {
            var options = new AddProductOptions();
            options.Id = $"123{DateTime.Now.Millisecond}";
            options.Name = "alex";
            options.Price = 1.2M;
            options.ProductCategory = Core.Model.ProductCategory.Computers;

            var result = psvc_.AddProduct(options);
            Assert.True(result);

            var p = psvc_.GetProductById(options.Id);
            Assert.NotNull(p);
            Assert.Equal(options.Name, p.Name);
            Assert.Equal(options.Price, p.Price);
            Assert.Equal(options.ProductCategory, p.Category);
        }

        [Fact]
        public void AddProduct_Fail_InvalidCategory()
        {
            var options = new AddProductOptions();
            options.Id = $"123{DateTime.Now.Millisecond}";
            options.Name = "alex";
            options.Price = 1.2M;

            var result = psvc_.AddProduct(options);
            Assert.False(result);
        }
    }
}
