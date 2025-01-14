using AUTHIO.DOMAIN.Enums;

namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Classe de entidade de Realm.
/// </summary>
public class RealmEntity : IEntityBase
{
    /// <summary>
    /// ctor
    /// </summary>
    public RealmEntity()
    {
        Roles = [];
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="created"></param>
    /// <param name="updated"></param>
    /// <param name="status"></param>
    /// <param name="name"></param>
    /// <param name="description"></param>
    public RealmEntity(Guid userId, 
        DateTime created, DateTime? updated,
        Status status, string name, string description)
    {
        UserId = userId;
        Created = created;
        Updated = updated;
        Status = status;
        Name = name;
        Description = description;
    }

    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Usuário de criação.
    /// </summary>
    public Guid UserId { get; set; }

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
    public virtual ICollection<ClientEntity> Clients { get; set; }

    /// <summary> 
    /// Roles vinculadas ao Client.
    /// </summary>
    public virtual ICollection<RoleEntity> Roles { get; private set; }
}
