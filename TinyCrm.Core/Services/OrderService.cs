using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IProductService products_;
        private readonly ICustomerService customers_;
        private readonly Data.TinyCrmDbContext context_;

        public OrderService(
            ICustomerService customers,
            IProductService products,
            Data.TinyCrmDbContext context)
        {
            context_ = context;
            customers_ = customers;
            products_ = products;
        }

        public async Task<ApiResult<Order>> CreateOrder(
            int customerId, ICollection<string> productIds)
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

            var products = new List<Product>();

            foreach (var p in productIds) {
                var presult = await products_
                    .GetProductByIdAsync(p);

                if (!presult.Success) {
                    var ret = presult.ToResult<Order>();

                    return new ApiResult<Order>(
                        presult.ErrorCode, presult.ErrorText);
                }

                products.Add(presult.Data);
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
            } catch (Exception ex) {
                return null;
            }

            return new ApiResult<Order>()
            {
                Data = order
            };
        }
    }
}
