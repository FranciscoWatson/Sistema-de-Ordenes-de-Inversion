using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SOI.Application.Validations;

namespace SOI.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        services.AddValidatorsFromAssemblyContaining<CrearOrdenValidator>();
        
        return services;
    }
}