using AUTHIO.APPLICATION.Application.Services.System;
using AUTHIO.APPLICATION.Domain.Contracts.Repository;
using AUTHIO.APPLICATION.Domain.Contracts.Repository.Base;
using AUTHIO.APPLICATION.Domain.Contracts.Services.System;
using AUTHIO.APPLICATION.DOMAIN.CONTRACTS.REPOSITORY;
using AUTHIO.APPLICATION.Infra.Repository;
using AUTHIO.APPLICATION.Infra.Repository.Base;
using AUTHIO.APPLICATION.INFRA.FEATUREFLAGS;
using AUTHIO.APPLICATION.INFRA.REPOSITORY;
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
            .AddScoped<IAuthenticationService, AuthenticationService>()
            //.AddScoped<ITenantService, TenantService>()
        // Repository
            .AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped(typeof(IGenerictEntityCoreRepository<>), typeof(GenericEntityCoreRepository<>))
            .AddScoped<ITenantRepository, TenantRepository>()
        // Infra
            .AddScoped<IFeatureFlags, FeatureFlagsProvider>();


        return services;
    }
}
