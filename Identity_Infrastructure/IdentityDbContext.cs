using Identity_Application.Models.AppSettingsModels;
using Identity_Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Text.Json;

namespace Identity_Infrastructure;

public class IdentityDbContext : DbContext
{
    private readonly IOptions<DatabaseSettings> _settings;

    public IdentityDbContext()
    {
        
    }

    public IdentityDbContext(DbContextOptions<IdentityDbContext> options, IOptions<DatabaseSettings> settings) : base(options)
    {
        _settings = settings;
    }

    public DbSet<Claim> Claim { get; set; }

    public DbSet<Role> Role { get; set; }

    public DbSet<Identity> Identity { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionStr = _settings.Value.ConnectionString;
        optionsBuilder.UseSqlServer(connectionStr);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public static List<T> SeedData<T>(string fileName)
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        var data = File.ReadAllText(path + $"/SeedData/{fileName}.json");

        return JsonSerializer.Deserialize<List<T>>(data);
    }
}