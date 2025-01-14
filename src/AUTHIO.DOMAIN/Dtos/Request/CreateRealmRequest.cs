namespace AUTHIO.DOMAIN.Dtos.Request;

/// <summary>
/// Request de criação de Realm.
/// </summary>
public sealed class CreateRealmRequest
{
    /// <summary>
    /// Nomde do Realm.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Descrição do Realm.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Email do Realm.
    /// </summary>
    public string Email { get; set; }
}