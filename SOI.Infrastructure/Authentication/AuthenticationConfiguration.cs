using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SOI.Domain.Authentication;

namespace SOI.Infrastructure.Authentication;
public static class AuthenticationConfiguration
{
    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfig = new JwtAuthenticationConfig();
        configuration.GetSection("JwtAuthentication").Bind(jwtConfig);
        
        services.AddSingleton(jwtConfig);

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(jwtConfig.SecretKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });
        
        return services;
    }
    
}