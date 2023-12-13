using Identity_Infrastructure;
using Microsoft.EntityFrameworkCore;
using Identity_Application;
using Identity_Application.Queries;

namespace Identity_Presentation.Bootstrap;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(conf => conf.RegisterServicesFromAssemblyContaining(typeof(GetAllIdentitiesQuery)));

        services.AddApplication().AddInfrastructure();

        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Default"));
        });

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:8010");
            });
        });

        return services;
    }
}