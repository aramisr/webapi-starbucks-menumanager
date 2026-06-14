using Core.mediatOR;
using Core.Mappy.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Starbucks.MenuManager.API.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatOR(typeof(DependencyInjection).Assembly);

            services.AddMapper();

            return services;
        }
    }
}
