using Identity_Application.Interfaces;
using Identity_Application.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Identity_Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}