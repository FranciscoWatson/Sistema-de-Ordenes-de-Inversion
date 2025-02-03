using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SOI.Infrastructure.Authentication;
using SOI.Infrastructure.Persistence;

namespace SOI.Infrastructure;

public static class InfrastructureLayerConfiguration
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthenticationServices(configuration);
        services.AddPersistenceServices(configuration);
        return services;
    }
}