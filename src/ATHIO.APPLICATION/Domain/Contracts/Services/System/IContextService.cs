using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;

namespace AUTHIO.APPLICATION.DOMAIN.CONTRACTS.SERVICES.SYSTEM;

/// <summary>
/// Interface de contexto de http.
/// </summary>
public interface IContextService
{
    /// <summary>
    /// Verifica se o usuário esta logado.
    /// </summary>
    public bool IsAuthenticated { get; }

    /// <summary>
    /// Recupera do tenantId do usuário logado.
    /// </summary>
    /// <returns></returns>
    Guid? GetCurrentTenantId();

    /// <summary>
    /// Recupera a tenantKey passada no Header.
    /// </summary>
    /// <returns></returns>
    public string GetCurrentTenantKey();

    /// <summary>
    /// Recupera o id do usuário logado.
    /// </summary>
    /// <returns></returns>
    Guid GetCurrentUserId();

    void SetTokenValidateParameters(
        MessageReceivedContext receivedContext,
        JwtBearerOptions options,
        IConfiguration configurations);
}
