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
    /// <param name="ClientIdentityConfigurationId"></param>
    /// <param name="created"></param>
    /// <param name="updated"></param>
    public UserIdentityConfigurationEntity(
        Guid ClientIdentityConfigurationId,
        DateTime created, DateTime? updated, bool requireUniqueEmail, string allowedUserNameCharacters)
    {
        ClientIdentityConfigurationId = ClientIdentityConfigurationId;
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
    /// Id do Client identity configuration.
    /// </summary>
    public Guid ClientIdentityConfigurationId { get; set; }

    /// <summary>
    /// Entidade do Client identity configuration.
    /// </summary>
    public virtual ClientIdentityConfigurationEntity ClientIdentityConfiguration { get; set; }
}
