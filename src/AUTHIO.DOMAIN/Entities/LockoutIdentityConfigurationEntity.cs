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
    public LockoutIdentityConfigurationEntity()
    {

    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="ClientIdentityConfigurationId"></param>
    /// <param name="created"></param>
    /// <param name="updated"></param>
    public LockoutIdentityConfigurationEntity(
        Guid ClientIdentityConfigurationId,
        DateTime created, DateTime? updated,
        bool allowedForNewUsers, int maxFailedAccessAttempts, TimeSpan defaultLockoutTimeSpan)
    {
        ClientIdentityConfigurationId = ClientIdentityConfigurationId;
        Created = created;
        Updated = updated;
        AllowedForNewUsers = allowedForNewUsers;
        MaxFailedAccessAttempts = maxFailedAccessAttempts;
        DefaultLockoutTimeSpan = defaultLockoutTimeSpan;
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="ClientIdentityConfigurationId"></param>
    /// <param name="created"></param>
    /// <param name="updated"></param>
    public LockoutIdentityConfigurationEntity(
        Guid ClientIdentityConfigurationId,
        DateTime created, DateTime? updated)
    {
        ClientIdentityConfigurationId = ClientIdentityConfigurationId;
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
    /// Id do Client identity configuration.
    /// </summary>
    public Guid ClientIdentityConfigurationId { get; set; }

    /// <summary>
    /// Entidade do Client identity configuration.
    /// </summary>
    public virtual ClientIdentityConfigurationEntity ClientIdentityConfiguration { get; private set; }
}
