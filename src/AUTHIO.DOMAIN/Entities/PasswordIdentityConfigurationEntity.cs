using Microsoft.AspNetCore.Identity;

namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Entidade de Password Identity Configuration.
/// </summary>
public class PasswordIdentityConfigurationEntity : PasswordOptions, IEntityPrimaryKey<Guid>
{
    /// <summary>
    /// ctor
    /// </summary>
    public PasswordIdentityConfigurationEntity()
    {

    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="ClientIdentityConfigurationId"></param>
    /// <param name="created"></param>
    /// <param name="updated"></param>
    /// <param name="requiredLength"></param>
    /// <param name="requireDigit"></param>
    /// <param name="requiredUniqueChars"></param>
    /// <param name="requireNonAlphanumeric"></param>
    /// <param name="requireLowercase"></param>
    /// <param name="requireUppercase"></param>
    public PasswordIdentityConfigurationEntity(
        Guid ClientIdentityConfigurationId,
        DateTime created, DateTime? updated, int requiredLength,
        bool requireDigit, int requiredUniqueChars, bool requireNonAlphanumeric,
        bool requireLowercase, bool requireUppercase)
    {
        ClientIdentityConfigurationId = ClientIdentityConfigurationId;
        Created = created;
        Updated = updated;
        RequiredLength = requiredLength;
        RequiredUniqueChars = requiredUniqueChars;
        RequireNonAlphanumeric = requireNonAlphanumeric;
        RequireLowercase = requireLowercase;
        RequireUppercase = requireUppercase;
        RequireDigit = requireDigit;
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
