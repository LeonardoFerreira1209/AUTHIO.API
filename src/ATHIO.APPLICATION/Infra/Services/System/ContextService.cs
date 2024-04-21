using AUTHIO.APPLICATION.DOMAIN.CONTRACTS.SERVICES.SYSTEM;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using MessageReceivedContext = Microsoft.AspNetCore.Authentication.JwtBearer.MessageReceivedContext;

namespace AUTHIO.APPLICATION.Infra.Services.System;

/// <summary>
/// Classe de contexto Http do sistema.
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
public class ContextService(
    IHttpContextAccessor httpContextAccessor) : IContextService
{
    /// <summary>
    /// Recupera o tenantId do usuário logado.
    /// </summary>
    /// <returns></returns>
    public Guid? GetCurrentTenantId()
    {
        if (IsAuthenticated)
        {
            var tenantId
                = httpContextAccessor.HttpContext?.User?
                    .FindFirstValue("TenantId");

            if (tenantId is not null)
                return Guid.Parse(tenantId);
        }

        return null;
    }

    /// <summary>
    /// Recupera a tenantKey passada no Header.
    /// </summary>
    /// <returns></returns>
    public string GetCurrentTenantKey() => httpContextAccessor.HttpContext?.Request?.Headers
                  ?.FirstOrDefault(header => header.Key.Equals("tenantkey")).Value;

    /// <summary>
    /// Verifica se o usuário esta logado.
    /// </summary>
    public bool IsAuthenticated
        => httpContextAccessor.HttpContext?
            .User?.Identity?.IsAuthenticated ?? false;

    /// <summary>
    /// Recupera o id do usuário logado.
    /// </summary>
    /// <returns></returns>
    public Guid GetCurrentUserId()
        => Guid.Parse(httpContextAccessor.HttpContext
            ?.User?.FindFirstValue(ClaimTypes.NameIdentifier));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receivedContext"></param>
    /// <param name="options"></param>
    /// <param name="configurations"></param>
    public void SetTokenValidateParameters(
        MessageReceivedContext receivedContext, 
        JwtBearerOptions options, 
        IConfiguration configurations)
    {
        var hasTenantKey =
                    !string.IsNullOrEmpty(
                        receivedContext.HttpContext
                            .Request.Headers
                                .FirstOrDefault(x => x.Key == "tenantkey").Key
                            );

        if (hasTenantKey)
        {
            options.SaveToken = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                LogValidationExceptions = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.FromHours(3),

                ValidIssuer = configurations.GetValue<string>("Auth:ValidIssuer"),
                ValidAudience = configurations.GetValue<string>("dfgsdgdfgdfgdf"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("fsdfsdfsfsdffsdfsdf"))
            };
        }
        else
        {
            options.TokenValidationParameters = new TokenValidationParameters
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
        }
    }
}
