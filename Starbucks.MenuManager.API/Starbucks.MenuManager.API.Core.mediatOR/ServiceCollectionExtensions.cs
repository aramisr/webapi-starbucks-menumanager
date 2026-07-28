using Microsoft.Extensions.DependencyInjection;
using Core.mediatOR.Contracts;
using System.Reflection;
using Scrutor;
using Core.MediatOR.Contracts;

namespace Core.mediatOR
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatOR(this IServiceCollection services, params Assembly[] assemblies) 
        {
            services.AddScoped<IMediator, Mediator>();

            services.Scan(scan => scan.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
            );

            services.Scan(scan => scan
                .FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo(typeof(IPipelineBehavior<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
            );

            return services;
        }
    }
}
