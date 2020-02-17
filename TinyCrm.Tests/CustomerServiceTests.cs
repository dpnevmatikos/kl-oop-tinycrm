using System;
using System.Linq;

using Xunit;
using Autofac;

using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Services;
using System.Threading.Tasks;
using TinyCrm.Core;

namespace TinyCrm.Tests
{
    public partial class CustomerServiceTests
        : IClassFixture<TinyCrmFixture>
    {
        private readonly ICustomerService csvc_;

        public CustomerServiceTests(TinyCrmFixture fixture)
        {
            csvc_ = fixture.Container.Resolve<ICustomerService>();
        }

        [Fact]
        public async Task CreateCustomer_Success()
        {
            var options = new CreateCustomerOptions()
            {
                VatNumber = $"123{DateTime.UtcNow.Millisecond:D6}",
                Email = "dsadas",
                FirstName = "Alex",
                LastName = "ath",
                Phone = "344234",
            };

            var result = await csvc_.CreateCustomerAsync(options);

            Assert.NotNull(result);

            var customer = csvc_.SearchCustomers(
                new SearchCustomerOptions()
                {
                    VatNumber = options.VatNumber
                }).SingleOrDefault();

            Assert.NotNull(customer);
            Assert.Equal(options.Email, customer.Email);
            Assert.Equal(options.Phone, customer.Phone);
            Assert.True(customer.IsActive);
        }
        [Fact]

        public async Task CreateCustomer_Fail_VatNumber()
        {

            var options = new CreateCustomerOptions()
            {
                Email = "dsadas",
                FirstName = "Alex",
                LastName = "ath",
                Phone = "344234",
            };
            var result = await csvc_.CreateCustomerAsync(options);

            Assert.Equal(StatusCode.BadRequest, result.ErrorCode);




        }
    }
}
