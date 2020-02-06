using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICustomerService customers_;
        private readonly Data.TinyCrmDbContext context_;

        public OrderService(
            ICustomerService customers,
            Data.TinyCrmDbContext context)
        {
            context_ = context;
            customers_ = customers;
        }

        public Order CreateOrder(int customerId,
            ICollection<string> productIds)
        {
            if (customerId <= 0) {
                return null;
            }

            if (productIds == null ||
              productIds.Count == 0) {
                return null;
            }

            var customer = customers_.SearchCustomers(
                new SearchCustomerOptions()
                {
                    CustomerId = customerId
                })
                .Where(c => c.IsActive)
                .SingleOrDefault();

            if (customer == null) {
                return null;
            }

            var products = context_
                .Set<Product>()
                .Where(p => productIds.Contains(p.Id))
                .ToList();

            if (products.Count != productIds.Count) {
                return null;
            }

            var order = new Order()
            {
                Customer = customer
            };

            foreach (var p in products) {
                order.Products.Add(
                    new OrderProduct()
                    {
                        ProductId = p.Id
                    });
            }

            context_.Add(order);

            try {
                context_.SaveChanges();
            } catch (Exception) {
                return null;
            }

            return order;
        }
    }
}
