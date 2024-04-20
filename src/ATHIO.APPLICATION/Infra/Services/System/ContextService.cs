using AUTHIO.APPLICATION.DOMAIN.CONTRACTS.SERVICES.SYSTEM;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

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
}
