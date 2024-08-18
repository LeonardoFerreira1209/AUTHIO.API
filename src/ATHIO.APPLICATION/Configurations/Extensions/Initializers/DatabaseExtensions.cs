using AUTHIO.INFRASTRUCTURE.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AUTHIO.APPLICATION.Configurations.Extensions.Initializers;

/// <summary>
/// Classe de extensão de configuração de dataBase na inicialização da aplicação.
/// </summary>
public static class DataBaseExtensions
{
    /// <summary>
    /// Executa a config da Base.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureDataBase(this IServiceCollection services, IConfiguration configurations)
        => services.AddDbContext<AuthIoContext>(options =>
        {
            string connectionString = Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? configurations
                    .GetConnectionString("DataBase");

            options.UseLazyLoadingProxies().UseMySQL(connectionString)
                        .LogTo(Console.WriteLine, LogLevel.Warning);

        }, ServiceLifetime.Scoped);
}
