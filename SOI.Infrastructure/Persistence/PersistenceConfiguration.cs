using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SOI.Application.Interfaces.Repositories;
using SOI.Infrastructure.Persistence.Repositories;


namespace SOI.Infrastructure.Persistence;

public static class PersistenceConfiguration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SoiDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"));
        });
        
        services.AddRepositories();

        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IOrdenRepository, OrdenRepository>();
        return services;
    }
}