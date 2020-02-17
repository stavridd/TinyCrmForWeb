using System;
using Autofac;

using TinyCrm.Core.Services;

namespace TinyCrm.Core
{

    // to autofac to bazv gia to autofac sto web
    // oles oi allages p eginan se sxesh me ta palia einai gia na
    // trexoun kai sta test kai sto web

    public class ServiceRegistrator : Module
    {
        public static void RegisterServices(
            ContainerBuilder builder)
        {
            if (builder == null) {
                throw new ArgumentNullException();
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

       public static IContainer CreateConteiner()
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
