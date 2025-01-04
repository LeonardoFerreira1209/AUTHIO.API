using AUTHIO.DOMAIN.Contracts.Factories;
using AUTHIO.DOMAIN.Contracts.Services.Infrastructure;
using AUTHIO.INFRASTRUCTURE.Factories;
using AUTHIO.INFRASTRUCTURE.Providers.Hangfire;
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
    public static IServiceCollection ConfigureHangfire(
        this IServiceCollection services, IConfiguration configurations)
    {
        services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseStorage(new MySqlStorage(
                    Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? configurations.GetConnectionString("DataBase"),
                        new MySqlStorageOptions {
                            QueuePollInterval = TimeSpan.FromSeconds(1),
                            JobExpirationCheckInterval = TimeSpan.FromHours(1),
                            CountersAggregateInterval = TimeSpan.FromMinutes(5),
                            PrepareSchemaIfNecessary = true,
                            DashboardJobListLimit = 25000,
                            TransactionTimeout = TimeSpan.FromMinutes(10),
                            TablesPrefix = "Hangfire"
                        }
                    )
                )

        ).AddHangfireServer();

        services.AddTransient<ITaskJobFactory, TaskJobFactory>();
        services.AddTransient<IHangFireJobsProvider, HangfireJobsProvider>();

        services.BuildServiceProvider()
            .GetService<IHangFireJobsProvider>().RegisterJobs();

        return services;
    }
}
