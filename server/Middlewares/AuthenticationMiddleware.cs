using KePass.Server.Services.Definitions;
using KePass.Server.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace KePass.Server.Middlewares;

public static class AuthenticationMiddleware
{
    public static IServiceCollection AddAuthenticationWithToken(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x =>
            {
                var environment = new HttpContextAccessor().HttpContext!.RequestServices
                    .GetRequiredService<IEnvironmentService>();

                var key = TokenService.GetSecretKey(environment);

                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

        return services;
    }
}