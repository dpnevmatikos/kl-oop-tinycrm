using System;
using System.Linq;

using Xunit;
using Autofac;

using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Services;

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
        public void CreateCustomer_Success()
        {
            var options = new CreateCustomerOptions()
            {
                VatNumber = $"123{DateTime.UtcNow.Millisecond:D6}",
                Email = "dsadas",
                FirstName = "Alex",
                LastName = "ath",
                Phone = "344234",
            };

            var result = csvc_.CreateCustomer(options);

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
    }
}
