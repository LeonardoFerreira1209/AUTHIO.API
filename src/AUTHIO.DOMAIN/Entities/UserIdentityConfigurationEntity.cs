using Microsoft.AspNetCore.Identity;

namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Entidade de User Identity Configuration.
/// </summary>
public class UserIdentityConfigurationEntity : UserOptions, IEntityPrimaryKey<Guid>
{
    /// <summary>
    /// ctor
    /// </summary>
    public UserIdentityConfigurationEntity()
    {

    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="tenantIdentityConfigurationId"></param>
    /// <param name="created"></param>
    /// <param name="updated"></param>
    public UserIdentityConfigurationEntity(
        Guid tenantIdentityConfigurationId,
        DateTime created, DateTime? updated, bool requireUniqueEmail, string allowedUserNameCharacters)
    {
        TenantIdentityConfigurationId = tenantIdentityConfigurationId;
        Created = created;
        Updated = updated;
        RequireUniqueEmail = requireUniqueEmail;
        AllowedUserNameCharacters = allowedUserNameCharacters ?? AllowedUserNameCharacters;
    }

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
    public DateTime? Updated { get; set; }

    /// <summary>
    /// Id do tenant identity configuration.
    /// </summary>
    public Guid TenantIdentityConfigurationId { get; set; }

    /// <summary>
    /// Entidade do tenant identity configuration.
    /// </summary>
    public virtual TenantIdentityConfigurationEntity TenantIdentityConfiguration { get; set; }
}
