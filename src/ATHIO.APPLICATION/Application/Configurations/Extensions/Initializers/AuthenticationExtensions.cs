using AUTHIO.APPLICATION.Domain.Contracts.Repositories;
using AUTHIO.APPLICATION.DOMAIN.CONTRACTS.SERVICES.SYSTEM;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using static AUTHIO.APPLICATION.Domain.Exceptions.CustomUserException;

namespace AUTHIO.APPLICATION.Application.Configurations.Extensions.Initializers;

/// <summary>
/// Classe de configuração do Autenticação da aplicação.
/// </summary>
public static class AuthenticationExtensions
{
    /// <summary>
    /// Configuração da autenticação do sistema.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configurations"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configurations)
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

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = async context =>
                {
                    IContextService contextService = context.HttpContext
                                .RequestServices
                                .GetRequiredService<IContextService>();

                    if (contextService.TryGetValueByHeader(
                        "Authorization", out StringValues authHeader))
                    {
                        var tenantKey = 
                            authHeader.ToString()
                                .GetTenantKeyByToken();

                        if (tenantKey is not null)
                        {
                            ITenantConfigurationRepository tenantService = 
                                context.HttpContext
                                .RequestServices
                                .GetRequiredService<ITenantConfigurationRepository>();

                            var tokenValidationParameters = 
                                await tenantService
                                .GetAsync(x => x.TenantKey == tenantKey);

                            context.Options.TokenValidationParameters = new TokenValidationParameters
                            {
                                LogValidationExceptions = true,
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidateLifetime = true,
                                ValidateIssuerSigningKey = true,
                                ClockSkew = TimeSpan.FromHours(3),

                                ValidIssuer = "sdfsdfsdfdsfdsfsdfsdfds",
                                ValidAudience = "sdfsdfsdfdsfdsfsdfsdfds",
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configurations.GetValue<string>("Auth:SecurityKey")))
                            };
                        }

                        return;
                    }

                    context.Options.TokenValidationParameters = new TokenValidationParameters
                    {
                        LogValidationExceptions = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.FromHours(3),

                        ValidIssuer = configurations.GetValue<string>("Auth:ValidIssuer"),
                        ValidAudience = configurations.GetValue<string>("Auth:ValidAudience"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configurations.GetValue<string>("Auth:SecurityKey")))
                    };
                },
                OnAuthenticationFailed = context =>
                {
                    Log.Error($"[LOG ERROR] {nameof(JwtBearerEvents)} - METHOD OnAuthenticationFailed - {context.Exception.Message}\n");

                    throw new UnauthorizedUserException("Token do usuário não é permitido ou está incorreto!");
                },
                OnTokenValidated = context =>
                {
                    Log.Information($"[LOG INFORMATION] {nameof(JwtBearerEvents)} - OnTokenValidated - {context.SecurityToken}\n");

                    return Task.CompletedTask;
                }
            };
        });

        return services;
    }

    /// <summary>
    /// Recupera um tenantKey do JwtToken.
    /// </summary>
    /// <param name="authHeader"></param>
    /// <returns></returns>
    private static string GetTenantKeyByToken(this string authHeader)
    {
        var handler = 
            new JwtSecurityTokenHandler();

        string tokenWithoutBearer 
            = authHeader.ToString()
                .Replace("Bearer ", "");

        var tokenJson 
            = handler.ReadToken(
                tokenWithoutBearer) as JwtSecurityToken;

        return tokenJson.Claims
            .FirstOrDefault(x =>
                x.Type == "tenantKey")?.Value;
    }
}
