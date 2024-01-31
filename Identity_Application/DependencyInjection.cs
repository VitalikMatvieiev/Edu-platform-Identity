using FluentValidation;
using Identity_Application.Interfaces.Authentication;
using Identity_Application.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Identity_Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IPasswordHasherService, PasswordHasherService>();

        var assembly = typeof(DependencyInjection).Assembly;

        services.AddAutoMapper(assembly);

        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(assembly));

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}