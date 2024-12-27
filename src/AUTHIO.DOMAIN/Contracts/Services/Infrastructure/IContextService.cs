using Microsoft.Extensions.Primitives;
using System.Diagnostics.CodeAnalysis;

namespace AUTHIO.DOMAIN.Contracts.Services.Infrastructure;

/// <summary>
/// Interface de contexto de http.
/// </summary>
public interface IContextService
{
    /// <summary>
    /// Verifica se o usuário esta logado.
    /// </summary>
    bool IsAuthenticated { get; }

    /// <summary>
    /// Permite autenticar por tenantKey.
    /// </summary>
    bool IsAuthByTenantKey { get; }

    /// <summary>
    /// Recupera do tenantId do usuário logado.
    /// </summary>
    /// <returns></returns>
    Guid? GetCurrentTenantId();

    /// <summary>
    /// Recupera a tenantKey passada no Header.
    /// </summary>
    /// <returns></returns>
    string GetCurrentTenantKey();

    /// <summary>
    /// Recupera a tenantKey passada no token.
    /// </summary>
    /// <returns></returns>
    string GetCurrentTenantKeyByToken();

    /// <summary>
    /// Recupera a tenantKey no header.
    /// </summary>
    string GetCurrentTenantKeyByHeader();

    /// <summary>
    ///  Recupera a tenantKey nas claims.
    /// </summary>
    /// <returns></returns>
    string GetCurrentTenantKeyByClaims();

    /// <summary>
    /// Recupera o id do usuário logado.
    /// </summary>
    /// <returns></returns>
    Guid GetCurrentUserId();

    /// <summary>
    /// Tenta Recuperar um valor do header pelo key/type.
    /// </summary>
    /// <param name="header"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    bool TryGetValueByHeader(string header, [MaybeNullWhen(false)] out StringValues value);

    /// <summary>
    /// Retorna a url base.
    /// </summary>
    /// <returns></returns>
    string GetUrlBase();
}
