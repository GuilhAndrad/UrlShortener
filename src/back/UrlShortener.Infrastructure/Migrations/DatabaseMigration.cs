using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace UrlShortener.Infrastructure.Migrations;

public class DatabaseMigration
{
    private const int MaxAttempts = 5;
    private static readonly TimeSpan DelayBetweenAttempts = TimeSpan.FromSeconds(3);

    public static void RunnerMigrations(IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(DatabaseMigration));

        for (var attempt = 1; attempt <= MaxAttempts; attempt++)
        {
            try
            {
                IMigrationRunner runner = serviceProvider.GetRequiredService<IMigrationRunner>();

                runner.ListMigrations();

                runner.MigrateUp();

                return;
            }
            catch (Exception exception) when (attempt < MaxAttempts)
            {
                logger.LogWarning(
                    exception,
                    "Failed to connect to the database (attempt {Attempt}/{MaxAttempts}). Retrying in {Delay}s...",
                    attempt, MaxAttempts, DelayBetweenAttempts.TotalSeconds);

                Thread.Sleep(DelayBetweenAttempts);
            }
        }
    }
}
