namespace AUTHIO.DOMAIN.Dtos.Request;

/// <summary>
/// Request de atualização de Realm.
/// </summary>
public sealed class UpdateRealmRequest
{
    /// <summary>
    /// Id do Realnm.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Nomde do Realnm.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Descrição do Realnm.
    /// </summary>
    public required string Description { get; set; }
}
