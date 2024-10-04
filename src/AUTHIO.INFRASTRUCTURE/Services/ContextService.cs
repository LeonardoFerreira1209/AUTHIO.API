using AUTHIO.DOMAIN.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace AUTHIO.INFRASTRUCTURE.Services;

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
                  ?.FirstOrDefault(header => header.Key.Equals("X-Tenant-KEY")).Value
                        ?? httpContextAccessor.HttpContext?
                                .User.Claims.FirstOrDefault(x => x.Issuer == "tenantkey").Value;

    public string GetEndpointRoute => httpContextAccessor.HttpContext.GetEndpoint().DisplayName;

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
    /// Tenta Recuperar um valor do header pelo key/type.
    /// </summary>
    /// <param name="header"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool TryGetValueByHeader(string key, [MaybeNullWhen(false)] out StringValues value)
        => httpContextAccessor
               .HttpContext.Request
                   .Headers.TryGetValue(key, out value);
}
