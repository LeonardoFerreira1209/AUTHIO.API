using AUTHIO.APPLICATION.Domain.Contracts.Repository.Base;
using AUTHIO.APPLICATION.Infra.Repository.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AUTHIO.APPLICATION.Application.Configurations.Extensions.Initializers;

/// <summary>
/// Classe de configuração do depêndencias da aplicação.
/// </summary>
public static class DependenciesExtensions
{
    /// <summary>
    /// Configuração das dependencias (Serrvices, Repository, Facades, etc...).
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureDependencies(
        this IServiceCollection services, IConfiguration configurations)
    {
        services
            .AddSingleton(serviceProvider => configurations)

        // Services

        // Repository
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped(typeof(IGenerictEntityCoreRepository<>), typeof(GenericEntityCoreRepository<>));
        // Infra

        return services;
    }
}
