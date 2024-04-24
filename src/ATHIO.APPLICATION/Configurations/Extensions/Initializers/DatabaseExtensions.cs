using AUTHIO.DATABASE.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AUTHIO.APPLICATION.Configurations.Extensions.Initializers;

/// <summary>
/// Classe de extensão de configuração de database na inicialização da aplicação.
/// </summary>
public static class DatabaseExtensions
{
    /// <summary>
    /// Executa a config da base.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configurations)
        => services.AddDbContext<AuthIoContext>(options =>
        {
            string connectionString = Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? configurations
                    .GetConnectionString("Database");

            options.UseLazyLoadingProxies().UseMySQL(connectionString)
                        .LogTo(Console.WriteLine, LogLevel.Debug);

        }, ServiceLifetime.Scoped);
}
