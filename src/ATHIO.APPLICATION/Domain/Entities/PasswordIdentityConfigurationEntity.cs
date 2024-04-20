using Microsoft.AspNetCore.Identity;

namespace AUTHIO.APPLICATION.Domain.Entities;

/// <summary>
/// Entidade de Password Identity Configuration.
/// </summary>
public class PasswordIdentityConfigurationEntity : PasswordOptions, IEntityPrimaryKey<Guid>
{
    /// <summary>
    /// ctor
    /// </summary>
    public PasswordIdentityConfigurationEntity() { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="tenantIdentityConfigurationId"></param>
    /// <param name="created"></param>
    /// <param name="updated"></param>
    /// <param name="requireUniqueEmail"></param>
    /// <param name="allowedUserNameCharacters"></param>
    public PasswordIdentityConfigurationEntity(
        Guid tenantIdentityConfigurationId,
        DateTime created, DateTime? updated, int requiredLength, 
        bool requireDigit, int requiredUniqueChars, bool requireNonAlphanumeric,
        bool requireLowercase, bool requireUppercase)
    {
        TenantIdentityConfigurationId = tenantIdentityConfigurationId;
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
    /// Id do tenant identity configuration.
    /// </summary>
    public Guid TenantIdentityConfigurationId { get; set; }

    /// <summary>
    /// Entidade do tenant identity configuration.
    /// </summary>
    public virtual TenantIdentityConfigurationEntity TenantIdentityConfiguration { get; set; }
}
