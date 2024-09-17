using AUTHIO.DOMAIN.Enums;

namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Classe de entidade de tenant.
/// </summary>
public class TenantEntity : IEntityBase
{
    /// <summary>
    /// ctor
    /// </summary>
    public TenantEntity()
    {
        Users = [];
        Roles = [];
    }

    public TenantEntity(Guid userId, 
        DateTime created, DateTime? updated,
        Status status, string name, string description,
        TenantConfigurationEntity tenantConfiguration)
    {
        UserId = userId;
        Created = created;
        Updated = updated;
        Status = status;
        Name = name;
        Description = description;
        TenantConfiguration = tenantConfiguration;
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
    /// Nomde do tenant.
    /// </summary>
    public string Name { get; set; } = null;

    /// <summary>
    /// Descrição do tenant.
    /// </summary>
    public string Description { get; set; } = null;

    /// <summary>
    /// Comfiguração do Tenant.
    /// </summary>
    public virtual TenantConfigurationEntity TenantConfiguration { get; set; }

    /// <summary>
    /// Users vinculados ao tenant.
    /// </summary>
    public virtual ICollection<UserEntity> Users { get; private set; }

    /// <summary>
    /// Users Admins vinculados ao tenant.
    /// </summary>
    public virtual ICollection<TenantIdentityUserAdminEntity> UserAdmins { get; private set; }

    /// <summary> 
    /// Roles vinculadas ao tenant.
    /// </summary>
    public virtual ICollection<RoleEntity> Roles { get; private set; }
}
