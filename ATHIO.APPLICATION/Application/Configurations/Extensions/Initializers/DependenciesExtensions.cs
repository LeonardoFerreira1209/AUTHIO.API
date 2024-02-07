using AUTHIO.APPLICATION.Application.Services;
using AUTHIO.APPLICATION.Domain.Contracts.Repository;
using AUTHIO.APPLICATION.Domain.Contracts.Repository.Base;
using AUTHIO.APPLICATION.Domain.Contracts.Services;
using AUTHIO.APPLICATION.Infra.FeaturesFlags;
using AUTHIO.APPLICATION.Infra.Repository;
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
            .AddScoped<ITenantService, TenantService>()
        // Repository
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped(typeof(IGenerictEntityCoreRepository<>), typeof(GenericEntityCoreRepository<>))
            .AddScoped<ITenantRepository, TenantRepository>()
        // Infra
            .AddScoped<IFeatureFlags, FeatureFlagsProvider>();


        return services;
    }
}
