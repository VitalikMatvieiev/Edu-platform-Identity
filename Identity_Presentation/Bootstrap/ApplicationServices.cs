using Identity_Infrastructure;
using Identity_Application;
using Identity_Application.Queries.Identities;
using Identity_Application.Models.AppSettingsModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Identity_Presentation.Bootstrap;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(conf =>
            conf.RegisterServicesFromAssemblyContaining(typeof(GetAllIdentitiesQuery)));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:TokenIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Securitykey"]))
                };
            });

        services.AddApplication().AddInfrastructure();

        services.AddAuthorization();

        services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.Configure<PasswordHashSettings>(configuration.GetSection("PasswordHashSettings"));

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