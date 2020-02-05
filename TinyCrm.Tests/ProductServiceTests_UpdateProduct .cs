using System;
using Xunit;
using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Data;

namespace TinyCrm.Tests
{
    public partial class ProductServiceTests
    {
        [Fact]
        public void UpdateProduct_Success()
        {
            var options = new UpdateProductOptions()
            {
                Description = "Lenovo",
                Price = 600.00M
            };
            var sucess = psvc_.UpdateProduct("1111955", options);
            Assert.True(sucess);
        }
    }
}
