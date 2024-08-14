using AUTHIO.DOMAIN.Auth;
using AUTHIO.DOMAIN.Contracts.Jwt;
using AUTHIO.DOMAIN.Dtos.Configurations;
using AUTHIO.DOMAIN.Store;
using AUTHIO.INFRASTRUCTURE.Context;
using AUTHIO.INFRASTRUCTURE.Services;
using AUTHIO.INFRASTRUCTURE.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AUTHIO.APPLICATION.Configurations.Extensions.Initializers;

/// <summary>
/// Classe de configuração do Autenticação da aplicação.
/// </summary>
public static class AuthenticationExtensions
{
    /// <summary>
    /// Configuração da autenticação do sistema.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;

            options.EventsType = typeof(CustomJwtBearerEvents);
        });

        services.AddAuthorizationBuilder()
            .AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());

        services
            .AddJwksManager()
            .UseJwtValidation()
            .PersistKeysToDatabaseStore<AuthIoContext>();

        return services;
    }

    /// <summary>
    /// Seta a criação de sign.
    /// </summary>
    /// <returns></returns>
    private static JwksBuilder AddJwksManager(this IServiceCollection services, Action<JwtOptions> action = null)
    {
        if (action != null)
            services.Configure(action);

        services.AddScoped<IJwtService, JwtService>();

        return new JwksBuilder(services);
    }

    /// <summary>
    /// Seta credencial do sign.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IJwksBuilder UseJwtValidation(this IJwksBuilder builder)
    {
        builder.Services.AddSingleton<IPostConfigureOptions<JwtBearerOptions>>(s => new JwtPostConfigureOptions(s));

        return builder;
    }

    /// <summary>
    /// Persite em memoria.
    /// </summary>
    /// <returns></returns>
    private static IJwksBuilder PersistKeysInMemory(this IJwksBuilder builder)
    {
        builder.Services.AddScoped<IJsonWebKeyStore, InMemoryStore>();

        return builder;
    }

    /// <summary>
    /// Persiste no banco de dados.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="credential">The credential.</param>
    /// <returns></returns>
    public static IJwksBuilder PersistKeysToDatabaseStore<TContext>(this IJwksBuilder builder) where TContext : DbContext, ISecurityKeyContext
    {
        builder.Services.AddScoped<IJsonWebKeyStore, DatabaseJsonWebKeyStore<TContext>>();

        return builder;
    }

    //public static IApplicationBuilder UseJwksDiscovery(this IApplicationBuilder app, string jwtDiscoveryEndpoint = "/jwks")
    //{
    //    if (!jwtDiscoveryEndpoint.StartsWith('/')) throw new ArgumentException("The Jwks URI must starts with '/'");

    //    app.Map(new PathString(jwtDiscoveryEndpoint), x =>
    //        x.UseMiddleware<JwtServiceDiscoveryMiddleware>());

    //    return app;
    //}
}
