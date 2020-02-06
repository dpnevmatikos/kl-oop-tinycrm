using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Services;

using System.Linq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System;

namespace TinyCrm.Tests
{
    public partial class CustomerServiceTests : IDisposable
    {
        private readonly ICustomerService csvc_;
        private readonly TinyCrmDbContext context;

        public CustomerServiceTests()
        {
            context = new TinyCrmDbContext();
            csvc_ = new CustomerService(context);
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

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
