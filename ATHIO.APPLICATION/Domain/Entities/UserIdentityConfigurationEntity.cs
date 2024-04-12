using AUTHIO.APPLICATION.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace AUTHIO.APPLICATION.Domain.Entities;

/// <summary>
/// 
/// </summary>
public class UserIdentityConfigurationEntity : UserOptions, IEntityBase
{
    /// <summary>
    /// ctor
    /// </summary>
    public UserIdentityConfigurationEntity() { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="tenantIdentityConfigurationId"></param>
    /// <param name="created"></param>
    /// <param name="updated"></param>
    /// <param name="status"></param>
    public UserIdentityConfigurationEntity(
        Guid tenantIdentityConfigurationId,
        DateTime created, DateTime? updated, Status status, bool requireUniqueEmail, string allowedUserNameCharacters)
    {
        TenantIdentityConfigurationId = tenantIdentityConfigurationId;
        Created = created;
        Updated = updated;
        Status = status;
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

    /// <summary>
    /// Status.
    /// </summary>
    public Status Status { get; set; }
}
