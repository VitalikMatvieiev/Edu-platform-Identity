using Identity_Infrastructure;
using Identity_Application;
using Identity_Application.Queries.Identities;
using Identity_Application.Models.AppSettingsModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Identity_Domain.Entities.Base;
using Microsoft.OpenApi.Models;

namespace Identity_Presentation.Bootstrap;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
         {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "My API",
                Version = "v1"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
               {
                 new OpenApiSecurityScheme
                 {
                   Reference = new OpenApiReference
                   {
                     Type = ReferenceType.SecurityScheme,
                     Id = "Bearer"
                   }
                 },
                 new string[] { }
               }
            });
         });

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
            options.AddPolicy("GatewayPolicy", policy =>
            {
                policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:8001");
            });
        });

        return services;
    }

    public async static Task<IServiceScope> dbConfiguration(this IServiceScope scope)
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<IdentityDbContext>();
        var logger = services.GetRequiredService<ILogger<Program>>();

        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured during migration");
        }

        if (!context.Claim.Any())
            context.Claim.AddRange(IdentityDbContext.SeedData<Claim>("ClaimsSeed"));
        if (!context.Role.Any())
            context.Role.AddRange(IdentityDbContext.SeedData<Role>("RolesSeed"));
        if (!context.Identity.Any())
            context.Identity.AddRange(IdentityDbContext.SeedData<Identity>("IdentitySeed"));

        return scope;
    }
}