using Autofac;

using TinyCrm.Core.Services;

namespace TinyCrm.Core
{
    public class ServiceRegistrator
    {
        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterType<ProductService>()
                .InstancePerLifetimeScope()
                .As<IProductService>();

            builder
                .RegisterType<CustomerService>()
                .InstancePerLifetimeScope()
                .As<ICustomerService>();

            builder
                .RegisterType<OrderService>()
                .InstancePerLifetimeScope()
                .As<IOrderService>();

            builder
                .RegisterType<Data.TinyCrmDbContext>()
                .InstancePerLifetimeScope()
                .AsSelf();

            return builder.Build();
        }
    }
}
