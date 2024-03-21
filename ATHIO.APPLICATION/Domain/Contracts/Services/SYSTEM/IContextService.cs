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
    /// Recupera a apiKey passada no Header.
    /// </summary>
    /// <returns></returns>
    public string GetCurrentApiKey();

    /// <summary>
    /// Recupera o id do usuário logado.
    /// </summary>
    /// <returns></returns>
    Guid GetCurrentUserId();
}
