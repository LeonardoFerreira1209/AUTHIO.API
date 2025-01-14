using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Enums;

namespace AUTHIO.DOMAIN.Dtos.Response;

/// <summary>
/// Classe de response de Realm.
/// </summary>
public class RealmResponse
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Data de criação.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Data de atualização.
    /// </summary>
    public DateTime? Updated { get; set; } = null;

    /// <summary>
    /// Status.
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// Nomde do Client.
    /// </summary>
    public string Name { get; set; } = null;

    /// <summary>
    /// Descrição do Client.
    /// </summary>
    public string Description { get; set; } = null;

    /// <summary>
    /// Clients vinculados ao Realm.
    /// </summary>
    public ICollection<ClientResponse> Clients { get; set; }
}
