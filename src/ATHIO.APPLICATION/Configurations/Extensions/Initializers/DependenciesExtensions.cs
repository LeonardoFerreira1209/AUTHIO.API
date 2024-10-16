using AUTHIO.APPLICATION.Services;
using AUTHIO.DOMAIN.Auth;
using AUTHIO.DOMAIN.Contracts.Factories;
using AUTHIO.DOMAIN.Contracts.Providers.Email;
using AUTHIO.DOMAIN.Contracts.Providers.ServiceBus;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Contracts.Services.External;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.INFRASTRUCTURE.Factories;
using AUTHIO.INFRASTRUCTURE.Providers;
using AUTHIO.INFRASTRUCTURE.Repositories;
using AUTHIO.INFRASTRUCTURE.Repositories.Base;
using AUTHIO.INFRASTRUCTURE.Repositories.Store;
using AUTHIO.INFRASTRUCTURE.ServiceBus;
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
        // Others    
            .AddSingleton(serviceProvider => configurations)
            .AddScoped<CustomJwtBearerEvents>()
        // Services
            .AddTransient<IFeatureFlagsService, FeatureFlagsService>()
            .AddTransient<IContextService, ContextService>()
            .AddTransient<IAuthenticationService, AuthenticationService>()
            .AddTransient<IUserService, UserService>()
            .AddTransient<ITokenService, TokenService>()
            .AddTransient<ITenantService, TenantService>()
            .AddTransient<IEventService, EventService>()
            .AddTransient<IStripeService, StripeService>()
        // Repository
            .AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
            .AddScoped(typeof(IGenerictEntityCoreRepository<>), typeof(GenericEntityCoreRepository<>))
            .AddScoped<IFeatureFlagsRepository, FeatureFlagsRepository>()
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
            .AddScoped<ICryptoService, CryptoService>()
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
