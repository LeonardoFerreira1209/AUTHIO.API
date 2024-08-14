using AUTHIO.DOMAIN.Contracts.Jwt;
using Microsoft.Extensions.DependencyInjection;

namespace AUTHIO.APPLICATION.Configurations.Extensions;

/// <summary>
/// Builder de Jwks
/// </summary>
/// <param name="services"></param>
public class JwksBuilder(
    IServiceCollection services) : IJwksBuilder
{
    /// <summary>
    /// Serviços.
    /// </summary>
    public IServiceCollection Services { get; }
        = services ?? throw new ArgumentNullException(nameof(services));
}