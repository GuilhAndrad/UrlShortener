using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Domain.Repositories;
using UrlShortener.Domain.Services;
using UrlShortener.Infrastructure.DataAccess;
using UrlShortener.Infrastructure.DataAccess.Repositories;
using UrlShortener.Infrastructure.Services;

namespace UrlShortener.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddFluentMigrator(services, configuration);
        AddRepositories(services);

        services.AddSingleton<IShortCodeGenerator, ShortCodeGenerator>();

        services.AddHealthChecks().AddDbContextCheck<UrlShortenerDbContext>("DbConnection");
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");

        services.AddDbContext<UrlShortenerDbContext>(options =>
            options.UseMySQL(connectionString!));
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");

        services.AddFluentMigratorCore()
            .ConfigureRunner(runner => runner
                .AddMySql5()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(DependencyInjectionExtension).Assembly).For.Migrations())
            .AddLogging(logging => logging.AddFluentMigratorConsole());
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IShortUrlReader, ShortUrlRepository>();
        services.AddScoped<IShortUrlWriter, ShortUrlRepository>();
    }
}