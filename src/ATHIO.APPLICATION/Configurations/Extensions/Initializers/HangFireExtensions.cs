using AUTHIO.DOMAIN.Contracts;
using AUTHIO.INFRASTRUCTURE.Jobs.Hangfire;
using Hangfire;
using Hangfire.MySql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AUTHIO.APPLICATION.Configurations.Extensions.Initializers;

/// <summary>
/// Hangfire extensions
/// </summary>
public static class HangFireExtensions
{
    /// <summary>
    /// Configura o hangfire para schedullers.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configurations"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureHamgfire(
        this IServiceCollection services, IConfiguration configurations)
    {
        services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseStorage(new MySqlStorage(
                    Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? configurations.GetConnectionString("Database"),
                        new MySqlStorageOptions
                        {
                            QueuePollInterval = TimeSpan.FromSeconds(10),
                            JobExpirationCheckInterval = TimeSpan.FromHours(1),
                            CountersAggregateInterval = TimeSpan.FromMinutes(5),
                            PrepareSchemaIfNecessary = true,
                            DashboardJobListLimit = 25000,
                            TransactionTimeout = TimeSpan.FromMinutes(1),
                            TablesPrefix = "Hangfire"
                        }
                    )
                )

            ).AddHangfireServer();

        services.AddTransient<IHangFireJobsProvider, HangfireJobsProvider>();

        services.BuildServiceProvider()
            .GetService<IHangFireJobsProvider>().RegisterJobs();

        return services;
    }
}
