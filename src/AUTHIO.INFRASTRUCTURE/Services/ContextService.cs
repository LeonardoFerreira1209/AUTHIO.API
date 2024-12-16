using AUTHIO.DOMAIN.Contracts.Services.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
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
    /// Lista de endpoints que devem ignorar a validação do token por tenantKey.
    /// </summary>
    private readonly IEnumerable<string> _dontUseTenantConfigAuthEndpoints = [
         "TenantController.RegisterTenantUserAsync",
         "UserController.GetAsync"
    ];

    /// <summary>
    /// Rota de acesso do endpoint.
    /// </summary>
    private string GetEndpointRoute => httpContextAccessor.HttpContext.GetEndpoint().DisplayName;

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
                  ?.FirstOrDefault(header => header.Key.Equals("X-Tenant-KEY", StringComparison.OrdinalIgnoreCase)).Value
                        ?? httpContextAccessor.HttpContext?
                                .User?.Claims?.FirstOrDefault(x => x.Issuer == "X-Tenant-KEY")?.Value;

    /// <summary>
    /// Recupera a tenantKey no Header.
    /// </summary>
    public string GetCurrentTenantKeyByHeader() => httpContextAccessor.HttpContext?.Request?.Headers
                  ?.FirstOrDefault(header => header.Key.Equals(
                      "X-Tenant-KEY", StringComparison.OrdinalIgnoreCase)).Value;

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

    /// <summary>
    /// Recupera o X-Tenant-KEY do token de usuário atual.
    /// </summary>
    /// <returns></returns>
    public string GetCurrentTenantKeyByToken()
    {
        if (TryGetValueByHeader(
                "Authorization", out StringValues authHeader))
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
                    x.Type == "X-Tenant-KEY")?.Value;
        }

        return null;
    }

    /// <summary>
    /// Permite autenticar por tenantKey. 
    /// </summary>
    public bool IsAuthByTenantKey =>
        !_dontUseTenantConfigAuthEndpoints
            .Any(x => GetEndpointRoute.Contains(x));
}
