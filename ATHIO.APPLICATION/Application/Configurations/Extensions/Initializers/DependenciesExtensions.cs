using AUTHIO.APPLICATION.Application.Services.System;
using AUTHIO.APPLICATION.APPLICATION.SERVICES;
using AUTHIO.APPLICATION.APPLICATION.SERVICES.SYSTEM;
using AUTHIO.APPLICATION.Domain.Contracts.Repositories;
using AUTHIO.APPLICATION.Domain.Contracts.Repositories.Base;
using AUTHIO.APPLICATION.Domain.Contracts.Services;
using AUTHIO.APPLICATION.Domain.Contracts.Services.System;
using AUTHIO.APPLICATION.DOMAIN.CONTRACTS.SERVICES.SYSTEM;
using AUTHIO.APPLICATION.Infra.Repositories;
using AUTHIO.APPLICATION.Infra.Repositories.BASE;
using AUTHIO.APPLICATION.INFRA.FEATUREFLAGS;
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
            .AddScoped<IContextService, ContextService>()
            .AddScoped<IAuthenticationService, AuthenticationService>()
            .AddScoped<ITokenService, TokenService>()
            .AddScoped<ITenantService, TenantService>()
        // Repository
            .AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped(typeof(IGenerictEntityCoreRepository<>), typeof(GenericEntityCoreRepository<>))
            .AddScoped<ITenantRepository, TenantRepository>()
            .AddScoped<ITenantConfigurationRepository, TenantConfigurationRepository>()
            .AddScoped<ITenantIdentityConfigurationRepository, TenantIdentityConfigurationRepository>()
            .AddScoped<IUserIdentityConfigurationRepository, UserIdentityConfigurationRepository>()
            .AddScoped<IPasswordIdentityConfigurationRepository, PasswordIdentityConfigurationRepository>()
            .AddScoped<IUserRepository, UserRepository>()
        // Infra
            .AddScoped<IFeatureFlags, FeatureFlagsProvider>();


        return services;
    }
}
