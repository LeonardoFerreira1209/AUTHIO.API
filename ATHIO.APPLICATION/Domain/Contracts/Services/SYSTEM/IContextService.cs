namespace AUTHIO.APPLICATION.DOMAIN.CONTRACTS.SERVICES.SYSTEM;

/// <summary>
/// Interface de contexto de http.
/// </summary>
public interface IContextService
{
    /// <summary>
    /// Recupera o id do usuário logado.
    /// </summary>
    /// <returns></returns>
    Guid GetCurrentUserId();

    /// <summary>
    /// Recupera do tenantId do usuário logado.
    /// </summary>
    /// <returns></returns>
    Guid GetCurrentTenantId();
}
