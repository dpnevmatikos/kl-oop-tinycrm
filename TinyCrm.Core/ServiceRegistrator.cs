using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using System;
using TinyCrm.Core.Services;

namespace TinyCrm.Core
{
    public class ServiceRegistrator : Module
    {
        public static void RegisterServices(
            ContainerBuilder builder)
        {
            if (builder == null) {
                throw new ArgumentNullException(nameof(builder));
            }

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
        }

        public static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            RegisterServices(builder);

            return builder.Build();
        }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterServices(builder);
        }
    }
}
