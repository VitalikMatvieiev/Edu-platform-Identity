using Identity_Application.Interfaces;
using Identity_Application.Interfaces.Authentication;
using Identity_Application.Interfaces.Repository;
using Identity_Infrastructure.Authentication;
using Identity_Infrastructure.Repositories;
using Identity_Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Identity_Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddSingleton<IJwtGenerator, JwtGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}