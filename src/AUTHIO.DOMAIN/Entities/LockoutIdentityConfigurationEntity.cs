using Microsoft.AspNetCore.Identity;

namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Entidade de Lockout Identity Configuration.
/// </summary>
public class LockoutIdentityConfigurationEntity : LockoutOptions, IEntityPrimaryKey<Guid>
{
    /// <summary>
    /// ctor
    /// </summary>
    public LockoutIdentityConfigurationEntity() { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="tenantIdentityConfigurationId"></param>
    /// <param name="created"></param>
    /// <param name="updated"></param>
    public LockoutIdentityConfigurationEntity(
        Guid tenantIdentityConfigurationId,
        DateTime created, DateTime? updated,
        bool allowedForNewUsers, int maxFailedAccessAttempts, TimeSpan defaultLockoutTimeSpan)
    {
        TenantIdentityConfigurationId = tenantIdentityConfigurationId;
        Created = created;
        Updated = updated;
        AllowedForNewUsers = allowedForNewUsers;
        MaxFailedAccessAttempts = maxFailedAccessAttempts;
        DefaultLockoutTimeSpan = defaultLockoutTimeSpan;
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="tenantIdentityConfigurationId"></param>
    /// <param name="created"></param>
    /// <param name="updated"></param>
    public LockoutIdentityConfigurationEntity(
        Guid tenantIdentityConfigurationId,
        DateTime created, DateTime? updated)
    {
        TenantIdentityConfigurationId = tenantIdentityConfigurationId;
        Created = created;
        Updated = updated;
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
