using Microsoft.Extensions.DependencyInjection;

namespace AUTHIO.DOMAIN.Contracts.Jwt;

/// <summary>
/// Interface de JwksBuilder
/// </summary>
public interface IJwksBuilder
{
    /// <summary>
    /// Serviços.
    /// </summary>
    IServiceCollection Services { get; }
}