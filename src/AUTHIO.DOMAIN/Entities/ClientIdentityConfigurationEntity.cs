namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Classe de entidade Client Identity Configuration.
/// </summary>
public class ClientIdentityConfigurationEntity : IEntityPrimaryKey<Guid>
{
    /// <summary>
    /// ctor
    /// </summary>
    public ClientIdentityConfigurationEntity()
    {

    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="ClientConfigurationId"></param>
    /// <param name="created"></param>
    /// <param name="updated"></param>
    public ClientIdentityConfigurationEntity(
        Guid ClientConfigurationId,
        DateTime created, DateTime? updated,
        UserIdentityConfigurationEntity userIdentityConfiguration,
        PasswordIdentityConfigurationEntity passwordIdentityConfiguration,
        LockoutIdentityConfigurationEntity lockoutIdentityConfiguration)
    {
        ClientConfigurationId = ClientConfigurationId;
        Created = created;
        Updated = updated;
        UserIdentityConfiguration = userIdentityConfiguration;
        PasswordIdentityConfiguration = passwordIdentityConfiguration;
        LockoutIdentityConfiguration = lockoutIdentityConfiguration;
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
    /// Id do Client configuration Id.
    /// </summary>
    public Guid ClientConfigurationId { get; set; }

    /// <summary>
    /// Entidade do Client configuration.
    /// </summary>
    public virtual ClientConfigurationEntity ClientConfiguration { get; set; }

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
