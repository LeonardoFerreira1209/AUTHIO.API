using AUTHIO.APPLICATION.DOMAIN.CONTRACTS.SERVICES.SYSTEM;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AUTHIO.APPLICATION.APPLICATION.SERVICES.SYSTEM;

/// <summary>
/// Classe de contexto Http do sistema.
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
public class ContextService(
    IHttpContextAccessor httpContextAccessor)
        : IContextService
{
    private readonly IHttpContextAccessor
        _httpContextAccessor = httpContextAccessor;

    /// <summary>
    /// Recupera o tenantId do usuário logado.
    /// </summary>
    /// <returns></returns>
    public Guid? GetCurrentTenantId()
    {
        if (IsAuthenticated) {
            var tenantId
                = _httpContextAccessor.HttpContext?.User?
                    .FindFirstValue("TenantId");

            if (tenantId is not null) 
                return Guid.Parse(tenantId); 
        }
        else
        {
            var code = _httpContextAccessor.HttpContext.Request?.Headers
                  .FirstOrDefault(header => header.Key.Equals("code")).Value;

            if (!string.IsNullOrEmpty(code))
                return Guid.Parse(code);
        }

        return null;
    }

    public bool IsAuthenticated
        => _httpContextAccessor.HttpContext
            ?.User?.Identity?.IsAuthenticated ?? false;

    /// <summary>
    /// Recupera o id do usuário logado.
    /// </summary>
    /// <returns></returns>
    public Guid GetCurrentUserId()
        => Guid.Parse(_httpContextAccessor.HttpContext?
        .User.FindFirstValue(ClaimTypes.NameIdentifier));
}
