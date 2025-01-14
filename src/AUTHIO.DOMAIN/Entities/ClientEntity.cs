using AUTHIO.DOMAIN.Enums;

namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Classe de entidade de Client.
/// </summary>
public class ClientEntity : IEntityBase
{
    /// <summary>
    /// ctor
    /// </summary>
    public ClientEntity()
    {
        Users = [];
        Roles = [];
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="realmId"></param>
    /// <param name="created"></param>
    /// <param name="updated"></param>
    /// <param name="status"></param>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="clientConfiguration"></param>
    public ClientEntity(Guid userId, Guid realmId,
        DateTime created, DateTime? updated,
        Status status, string name, string description,
        ClientConfigurationEntity clientConfiguration)
    {
        UserId = userId;
        RealmId = realmId;
        Created = created;
        Updated = updated;
        Status = status;
        Name = name;
        Description = description;
        ClientConfiguration = clientConfiguration;
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
    /// Id o realm.
    /// </summary>
    public Guid RealmId { get; set; }

    /// <summary>
    /// Realm a qual o client pertence.
    /// </summary>
    public virtual RealmEntity Realm { get; set; }

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
    /// Comfiguração do Client.
    /// </summary>
    public virtual ClientConfigurationEntity ClientConfiguration { get; set; }

    /// <summary>
    /// Users vinculados ao Client.
    /// </summary>
    public virtual ICollection<UserEntity> Users { get; private set; }

    /// <summary>
    /// Users Admins vinculados ao Client.
    /// </summary>
    public virtual ICollection<ClientIdentityUserAdminEntity> UserAdmins { get; private set; }

    /// <summary> 
    /// Roles vinculadas ao Client.
    /// </summary>
    public virtual ICollection<RoleEntity> Roles { get; private set; }
}
