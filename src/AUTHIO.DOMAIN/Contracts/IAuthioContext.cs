namespace AUTHIO.DOMAIN.Contracts;

/// <summary>
/// Interface de AuthioContext.
/// </summary>
public interface IAuthioContext
{
    /// <summary>
    /// Usuario id do logado.
    /// </summary>
    public Guid? CurrentUserId { get; init; }

    /// <summary>
    /// Id do tenant atual.
    /// </summary>
    public Guid? TenantId { get; init; }

    /// <summary>
    /// Chave do tenant atual.
    /// </summary>
    public string TenantKey { get; init; }
}
