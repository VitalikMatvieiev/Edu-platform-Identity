using Identity_Infrastructure;
using Identity_Application;
using Identity_Application.Queries.Identities;
using Identity_Application.Models.AppSettingsModels;

namespace Identity_Presentation.Bootstrap;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(conf =>
            conf.RegisterServicesFromAssemblyContaining(typeof(GetAllIdentitiesQuery)));

        services.AddApplication().AddInfrastructure();

        services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddDbContext<IdentityDbContext>();

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