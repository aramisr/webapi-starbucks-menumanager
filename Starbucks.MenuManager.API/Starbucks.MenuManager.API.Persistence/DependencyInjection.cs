using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Starbucks.MenuManager.API.Persistence.Contexts;

namespace Starbucks.MenuManager.API.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<StarbucksDbContext>(options => {
                options.UseSqlite(configuration.GetConnectionString("SqliteDatabase"));
            });

            return services;
        } 
    }
}
