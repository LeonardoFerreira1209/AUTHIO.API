namespace AUTHIO.DOMAIN.Contracts.Services.Infrastructure;

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
    /// Id do Client atual.
    /// </summary>
    public Guid? ClientId { get; init; }

    /// <summary>
    /// Chave do Client atual.
    /// </summary>
    public string ClientKey { get; init; }
}
