using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Dtos.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using static AUTHIO.DOMAIN.Exceptions.CustomUserException;

namespace AUTHIO.DOMAIN.Auth;

/// <summary>
/// Classe custom de tratamentos de eventos de token bearer.
/// </summary>
/// <param name="contextService"></param>
/// <param name="configurations"></param>
public class CustomJwtBearerEvents(
    IContextService contextService, 
    IOptions<AppSettings> configurations) : JwtBearerEvents
{
    /// <summary>
    /// Autenticação do token falhou.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="UnauthorizedUserException"></exception>
    public override Task AuthenticationFailed(AuthenticationFailedContext context)
    {
        Log.Error($"[LOG ERROR] {nameof(JwtBearerEvents)} - METHOD OnAuthenticationFailed - {context.Exception.Message}\n");

        throw new UnauthorizedUserException(
            "Token do usuário não é permitido ou está incorreto!");
    }

    /// <summary>
    /// Token validado.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override Task TokenValidated(TokenValidatedContext context)
    {
        Log.Information($"[LOG INFORMATION] {nameof(JwtBearerEvents)} - OnTokenValidated - {context.SecurityToken}\n");

        return Task.CompletedTask;
    }

    /// <summary>
    /// Recebimento de requisição.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override Task MessageReceived(MessageReceivedContext context)
    {
        if (contextService.TryGetValueByHeader(
                       "Authorization", out StringValues authHeader))
        {
            var tenantKey =
                GetTenantKeyByToken(authHeader.ToString());

            if (tenantKey is not null)
            {
                ITenantConfigurationRepository tenantConfigurationRepository =
                    context.HttpContext
                        .RequestServices
                            .GetRequiredService<ITenantConfigurationRepository>();

                var tenantConfigurationEntity =
                    tenantConfigurationRepository
                        .GetAsync(x => x.TenantKey == tenantKey).Result;

                var tenantTokenConfiguration =
                    tenantConfigurationEntity.TenantTokenConfiguration;

                context.Options.TokenValidationParameters = new TokenValidationParameters
                {
                    LogValidationExceptions = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.FromHours(3),

                    ValidIssuer = tenantTokenConfiguration.Issuer,
                    ValidAudience = tenantTokenConfiguration.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tenantTokenConfiguration.SecurityKey))
                };
            }

            return Task.CompletedTask;
        }

        context.Options.TokenValidationParameters = new TokenValidationParameters
        {
            LogValidationExceptions = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.FromHours(3),

            ValidIssuer = configurations.Value.Auth.ValidIssuer,
            ValidAudience = configurations.Value.Auth.ValidAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configurations.Value.Auth.SecurityKey))
        };

        return Task.CompletedTask;
    }

    /// <summary>
    /// Recupera um tenantKey do JwtToken.
    /// </summary>
    /// <param name="authHeader"></param>
    /// <returns></returns>
    private static string GetTenantKeyByToken(string authHeader)
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
