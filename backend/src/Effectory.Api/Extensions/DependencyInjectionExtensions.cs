using Effectory.Infra.Repository.Mapping;
using Effectory.Infra.ServiceBus;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Effectory.Api.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void RegisterAllTypes<T>(this IServiceCollection services, Assembly[] assemblies,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));
            foreach (var type in typesFromAssemblies)
                services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
        }

        public static void RegisterServiceBus(this IServiceCollection services, IConfiguration config)
        {
            services.AddMassTransit(cfg =>
            {
                cfg.AddBus(ctx => Bus.Factory.CreateUsingRabbitMq(bus =>
                {
                    var configuration = config.GetSection("ServiceBus");
                    var consumer = ctx.GetService<ISubscribe>();

                    bus.Host(configuration["Url"], c =>
                    {
                        c.Username(configuration["Username"]);
                        c.Password(configuration["Password"]);
                    });


                    bus.ReceiveEndpoint("effectory", e =>
                    {
                        e.Handler<BusMessage>(ctx => consumer.HandleMessage(ctx.Message));

                        EndpointConvention.Map<BusMessage>(e.InputAddress);
                    });
                }));
            });
        }

        public static void RunMappers(this IApplicationBuilder applicationBuilder)
        {
            var entityMappers = applicationBuilder.ApplicationServices.GetRequiredService<IEnumerable<IEntityMapper>>();
            foreach (var mapping in entityMappers) mapping.Map();
        }
    }
}
