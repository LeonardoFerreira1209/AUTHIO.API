namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Classe de entidade Tenant Identity Configuration.
/// </summary>
public class TenantIdentityConfigurationEntity : IEntityPrimaryKey<Guid>
{
    /// <summary>
    /// ctor
    /// </summary>
    public TenantIdentityConfigurationEntity() { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="tenantConfigurationId"></param>
    /// <param name="created"></param>
    /// <param name="updated"></param>
    public TenantIdentityConfigurationEntity(
        Guid tenantConfigurationId,
        DateTime created, DateTime? updated)
    {
        TenantConfigurationId = tenantConfigurationId;
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
    /// Id do tenant configuration Id.
    /// </summary>
    public Guid TenantConfigurationId { get; set; }

    /// <summary>
    /// Entidade do tenant configuration.
    /// </summary>
    public virtual TenantConfigurationEntity TenantConfiguration { get; set; }

    /// <summary>
    /// Entidade de user identity configuration.
    /// </summary>
    public virtual UserIdentityConfigurationEntity UserIdentityConfiguration { get; set; }

    /// <summary>
    ///  Entidade de password identity configuration.
    /// </summary>
    public virtual PasswordIdentityConfigurationEntity PasswordIdentityConfiguration { get; set; }

    /// <summary>
    ///  Entidade de Lockout identity configuration.
    /// </summary>
    public virtual LockoutIdentityConfigurationEntity LockoutIdentityConfiguration { get; set; }
}
