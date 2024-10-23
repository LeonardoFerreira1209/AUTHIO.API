using Microsoft.Extensions.DependencyInjection;

namespace AUTHIO.APPLICATION.Configurations.Extensions.Initializers;

// <summary>
/// Classe de configuração do Cors da aplicação.
/// </summary>
public static class CorsExtensions
{
    /// <summary>
    /// Configuração dos cors aceitos.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureCors(this IServiceCollection services)
        => services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );
        });
}
