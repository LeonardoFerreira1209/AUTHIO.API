using AUTHIO.APPLICATION.Services;
using AUTHIO.DATABASE.Repositories;
using AUTHIO.DATABASE.Repositories.BASE;
using AUTHIO.DOMAIN.Contracts.Factories;
using AUTHIO.DOMAIN.Contracts.Providers.Email;
using AUTHIO.DOMAIN.Contracts.Providers.ServiceBus;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.INFRASTRUCTURE.Factories;
using AUTHIO.INFRASTRUCTURE.Providers;
using AUTHIO.INFRASTRUCTURE.ServiceBus;
using AUTHIO.INFRASTRUCTURE.Services;
using AUTHIO.INFRASTRUCTURE.Services.Identity;
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
            .AddTransient<IFeatureFlagsService, FeatureFlagsService>()
            .AddTransient<IContextService, ContextService>()
            .AddTransient<IAuthenticationService, AuthenticationService>()
            .AddTransient<IUserService, UserService>()
            .AddTransient<ITokenService, TokenService>()
            .AddTransient<ITenantService, TenantService>()
            .AddTransient<IEventService, EventService>()
        // Repository
            .AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
            .AddScoped<IFeatureFlagsRepository, FeatureFlagsRepository>()
            .AddScoped(typeof(IGenerictEntityCoreRepository<>), typeof(GenericEntityCoreRepository<>))
            .AddScoped<ITenantRepository, TenantRepository>()
            .AddScoped<ITenantConfigurationRepository, TenantConfigurationRepository>()
            .AddScoped<ITenantIdentityConfigurationRepository, TenantIdentityConfigurationRepository>()
            .AddScoped<IUserIdentityConfigurationRepository, UserIdentityConfigurationRepository>()
            .AddScoped<IPasswordIdentityConfigurationRepository, PasswordIdentityConfigurationRepository>()
            .AddScoped<ILockoutIdentityConfigurationRepository, LockoutIdentityConfigurationRepository>()
            .AddScoped<ITenantEmailConfigurationRepository, TenantEmailConfigurationRepository>()
            .AddScoped<ITenantTokenConfigurationRepository, TenantTokenConfigurationRepository>()
            .AddScoped<ISendGridConfigurationRepository, SendGridConfigurationRepository>()
            .AddScoped<IEventRepository, EventRepository>()
            .AddScoped<IFeatureFlagsRepository, FeatureFlagsRepository>()
            .AddScoped<ICustomUserStore<UserEntity>, CustomUserStore<UserEntity>>()
        // Infra
            .AddTransient<IEmailProvider, EmailProvider>()
            .AddTransient<IEmailProviderFactory, EmailProviderFactory>()
            .AddTransient<IEventFactory, EventFactory>()
            .AddTransient<IEventServiceBusProvider, EventServiceBusProvider>()
            .AddSingleton<ICachingService, CachingService>()
            .AddSingleton<EventServiceBusSubscriber>();

        return services;
    }
}
