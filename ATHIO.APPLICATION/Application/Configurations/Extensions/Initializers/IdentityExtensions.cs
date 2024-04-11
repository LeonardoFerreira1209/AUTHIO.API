using AUTHIO.APPLICATION.Application.Services.Custom;
using AUTHIO.APPLICATION.Domain.Entities;
using AUTHIO.APPLICATION.Infra.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AUTHIO.APPLICATION.Application.Configurations.Extensions.Initializers;

/// <summary>
/// Classe de configuração do Idenity da aplicação.
/// </summary>
public static class IdentityExtensions
{
    /// <summary>
    /// Configuração do identity server do sistema.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureIdentityServer(
        this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddIdentity<UserEntity, RoleEntity>(options =>
            {
                options.SignIn.RequireConfirmedEmail
                    = configuration
                        .GetValue<bool>("Signin:RequireConfirmedEmail");

                options.SignIn.RequireConfirmedAccount
                    = configuration
                        .GetValue<bool>("Signin:RequireConfirmedAccount");

                options.User.AllowedUserNameCharacters
                    = configuration
                        .GetValue<string>("User:AllowedUserNameCharacters");

                options.User.RequireUniqueEmail
                    = configuration
                        .GetValue<bool>("Signin:RequireConfirmedEmail");

                options.Stores.MaxLengthForKeys
                    = configuration
                        .GetValue<int>("Stores:MaxLengthForKeys");

                options.Lockout.DefaultLockoutTimeSpan
                    = TimeSpan.FromMinutes(configuration
                        .GetValue<int>("Lockout:DefaultLockoutTimeSpan"));

                options.Lockout.MaxFailedAccessAttempts
                    = configuration
                        .GetValue<int>("Lockout:MaxFailedAccessAttempts");

                options.Lockout.AllowedForNewUsers
                    = configuration
                        .GetValue<bool>("Lockout:AllowedForNewUsers");

                options.Password.RequireDigit
                    = configuration
                        .GetValue<bool>("Password:RequireDigit");

                options.Password.RequireLowercase
                    = configuration
                        .GetValue<bool>("Password:RequireLowercase");

                options.Password.RequireUppercase
                    = configuration
                        .GetValue<bool>("Password:RequireUppercase");

                options.Password.RequiredLength
                    = configuration
                        .GetValue<int>("Password:RequiredLength");

                options.Password.RequireNonAlphanumeric
                    = configuration
                        .GetValue<bool>("Password:RequireNonAlphanumeric");

                options.Password.RequiredUniqueChars
                    = configuration
                        .GetValue<int>("Password:RequiredUniqueChars");

            })
              .AddEntityFrameworkStores<AuthIoContext>()
              .AddDefaultTokenProviders()
              .AddSignInManager<CustomSignInManager>()
              .AddUserManager<CustomUserManager<UserEntity>>()
              .AddUserValidator<CustomUserValidator<UserEntity>>();

        services.AddScoped<CustomUserValidator<UserEntity>>();

        return services;
    }
}
