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

    public ClientEntity(Guid userId, 
        DateTime created, DateTime? updated,
        Status status, string name, string description,
        ClientConfigurationEntity ClientConfiguration)
    {
        UserId = userId;
        Created = created;
        Updated = updated;
        Status = status;
        Name = name;
        Description = description;
        this.ClientConfiguration = ClientConfiguration;
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
