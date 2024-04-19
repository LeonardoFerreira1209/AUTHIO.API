using AUTHIO.APPLICATION.Application.Services;
using AUTHIO.APPLICATION.Domain.Contracts.Repositories;
using AUTHIO.APPLICATION.Domain.Contracts.Repositories.Base;
using AUTHIO.APPLICATION.Domain.Contracts.Services;
using AUTHIO.APPLICATION.Domain.Contracts.Services.System;
using AUTHIO.APPLICATION.DOMAIN.CONTRACTS.SERVICES.SYSTEM;
using AUTHIO.APPLICATION.Infra.Repositories;
using AUTHIO.APPLICATION.Infra.Repositories.BASE;
using AUTHIO.APPLICATION.Infra.Services.System;
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
            .AddScoped<IFeatureFlagsService, FeatureFlagsService>()
            .AddScoped<IContextService, ContextService>()
            .AddScoped<IAuthenticationService, AuthenticationService>()
            .AddScoped<ITokenService, TokenService>()
            .AddScoped<ITenantService, TenantService>()
        // Repository
            .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
            .AddTransient<IFeatureFlagsRepository, FeatureFlagsRepository>()
            .AddTransient<IUserRepository, UserRepository>()
            .AddTransient(typeof(IGenerictEntityCoreRepository<>), typeof(GenericEntityCoreRepository<>))
            .AddTransient<ITenantRepository, TenantRepository>()
            .AddTransient<ITenantConfigurationRepository, TenantConfigurationRepository>()
            .AddTransient<ITenantIdentityConfigurationRepository, TenantIdentityConfigurationRepository>()
            .AddTransient<IUserIdentityConfigurationRepository, UserIdentityConfigurationRepository>()
            .AddTransient<IPasswordIdentityConfigurationRepository, PasswordIdentityConfigurationRepository>()
            .AddTransient<ILockoutIdentityConfigurationRepository, LockoutIdentityConfigurationRepository>()
            .AddTransient<IUserRepository, UserRepository>()
        // Infra
            .AddScoped<IFeatureFlagsRepository, FeatureFlagsRepository>();


        return services;
    }
}
