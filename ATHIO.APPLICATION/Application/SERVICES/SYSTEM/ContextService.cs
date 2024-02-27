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
    public Guid GetCurrentTenantId()
        => Guid.Parse(_httpContextAccessor.HttpContext?
        .User.Claims.FirstOrDefault(
            claim => claim.Type == "TenantId").Value);

    /// <summary>
    /// Recupera o id do usuário logado.
    /// </summary>
    /// <returns></returns>
    public Guid GetCurrentUserId() 
        => Guid.Parse(_httpContextAccessor.HttpContext?
        .User.FindFirstValue(ClaimTypes.NameIdentifier));
}
