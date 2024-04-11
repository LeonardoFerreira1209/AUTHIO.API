using AUTHIO.APPLICATION.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AUTHIO.APPLICATION.Application.Configurations.Extensions.Initializers;

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
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<AuthIoContext>(options =>
        {
            options.UseLazyLoadingProxies().UseMySQL(configuration
                    .GetConnectionString("Database"))
                        .LogTo(Console.WriteLine, LogLevel.Debug);

        }, ServiceLifetime.Scoped);
}
