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
    /// Permite autenticar por ClientKey.
    /// </summary>
    bool IsAuthByClientKey { get; }

    /// <summary>
    /// Recupera do ClientId do usuário logado.
    /// </summary>
    /// <returns></returns>
    Guid? GetCurrentClientId();

    /// <summary>
    /// Recupera a ClientKey passada no Header.
    /// </summary>
    /// <returns></returns>
    string GetCurrentClientKey();

    /// <summary>
    /// Recupera a ClientKey passada no token.
    /// </summary>
    /// <returns></returns>
    string GetCurrentClientKeyByToken();

    /// <summary>
    /// Recupera a ClientKey no header.
    /// </summary>
    string GetCurrentClientKeyByHeader();

    /// <summary>
    ///  Recupera a ClientKey nas claims.
    /// </summary>
    /// <returns></returns>
    string GetCurrentClientKeyByClaims();

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
