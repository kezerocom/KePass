using Microsoft.AspNetCore.Authentication;

namespace KePass.Server.Middlewares;

public static class AuthenticationMiddleware
{
    public static IServiceCollection AddAuthenticationWithToken(this IServiceCollection services)
    {
        return services;
    }
}