using AUTHIO.APPLICATION.Services;
using AUTHIO.DATABASE.Repositories;
using AUTHIO.DATABASE.Repositories.BASE;
using AUTHIO.DOMAIN.Contracts.Factories;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.INFRASTRUCTURE.Factories;
using AUTHIO.INFRASTRUCTURE.Providers;
using AUTHIO.INFRASTRUCTURE.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AUTHIO.APPLICATION.Configurations.Extensions.Initializers;

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
            .AddScoped<IFeatureFlagsRepository, FeatureFlagsRepository>()
            .AddScoped<IEmailProvider, EmailProvider>()
            .AddSingleton<IEmailProviderFactory, EmailProviderFactory>();

        return services;
    }
}
