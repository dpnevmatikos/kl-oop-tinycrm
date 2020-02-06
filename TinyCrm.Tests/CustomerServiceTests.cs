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
        }

        [Fact]
        public void Customer_Order_Success()
        {
            var p = new Product()
            {
                Id = $"1234{DateTime.Now.Millisecond}",
                Category = ProductCategory.Computers,
                Name = $"Product {DateTime.Now.Millisecond}",
                Price = 1234.44M
            };
            context.Add(p);

            var p2 = new Product()
            {
                Id = $"2234{DateTime.Now.Millisecond}",
                Category = ProductCategory.Computers,
                Name = $"Product {DateTime.Now.Millisecond}",
                Price = 12.44M
            };
            context.Add(p2);

            var customer = new Customer()
            {
                VatNumber = "117003949",
                Email = "dpnevmatikos@codehub.gr",
            };

            var order = new Order()
                {
                    DeliveryAddress = "Kleemann Kilkis"
                };

            order.Products.Add(
                new OrderProduct()
                {
                    Product = p2
                });

            customer.Orders.Add(order);

            context.Add(customer);
            context.SaveChanges();
        }

        [Fact]
        public void Customer_Order_Retrieve()
        {
            var orders = context
                .Set<OrderProduct>()
                .Where(c => c.Order.Customer.VatNumber == "117003949")
                .Select(c => c.Order.Products)
                .ToList();

            Assert.NotNull(orders);
        }

        [Fact]
        public void Orders_Retrieve()
        {
            var orders = context
                .Set<Order>()
                .Include(o => o.Customer)
                .ToList();
        }

        [Fact]
        public void AddCustomer_Success()
        {
            var result = csvc_.CreateCustomer(
                new CreateCustomerOptions()
                {
                    Email = "dpnevmatikos@codehub.gr",
                    VatNumber = "117003949"
                });

            Assert.True(result);
        }

        [Fact]
        public int AddCustomerContacts()
        {
            var customer = new Customer()
            {
                Firstname = "George",
                Lastname = "Stathis",
                Email = "george@gmail.com"
            };
            var Contacts = new ContactPerson()
            {
                Email = "pnevmatikos@gmail.com"
            };
            customer.Contacts.Add(Contacts);
            context.Add(customer);
            context.SaveChanges();

            return customer.Id;
        }

        [Fact]

        public void RetrieveContacts() 
        {
            var customerId = AddCustomerContacts();
            var contacts = context
                .Set<Customer>()
                .Include(c => c.Contacts)
                .Where(c => c.Id == customerId)
                .Select(c=>c.Contacts)
                .ToList();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
